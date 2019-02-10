
from __future__ import print_function

import grpc
import aconcagua_pb2
import aconcagua_pb2_grpc
from google.protobuf import empty_pb2 as googleEmpty

import pandas as pd
import matplotlib.pyplot as plt


def createmetadatarequest(seriesSource, seriesCodes, headers):
    request = aconcagua_pb2.GetMetadataRequest()
    request.requestmetadata['version'] = '0.9'
    request.metadataheaders.extend(headers)
    
    # TODO [jc]: find better way of projecting onto the list 
    ssk = aconcagua_pb2.SourceSeriesKey(sourcename = seriesSource)
    for s in seriesCodes :
        ssk.seriesname = s
        request.keys.extend([ssk])

    return request

def createobservationrequest(seriesSource, seriesCodes, frequencies):
    request = aconcagua_pb2.GetObservationsRequest()
    request.requestmetadata['version'] = '0.9'
    request.frequencies = frequencies
    
    # TODO [jc]: find better way of projecting onto the list 
    ssk = aconcagua_pb2.SourceSeriesKey(sourcename = seriesSource)
    for s in seriesCodes :
        ssk.seriesname = s
        request.keys.extend([ssk])

    return request

def showmetadataresponse(response):
    i = 0
    for ts in response.seriesdata:
        print('[%02d] source[series]: %s[%s]' % (i, ts.key.sourcename, ts.key.seriesname))

        for m,d in zip(response.metadataheaders, ts.values):
            print('    %s: %s' % (m,d))
        i += 1

def showobservationsresponse(response):
    i = 0
    for ts in response.seriesdata:
        print('[%02d] source[series]: %s[%s]' % (i, ts.key.sourcename, ts.key.seriesname))

        for key in ts.values:
            print('    %s: %d' % (key, ts.values[key]))
        i = i + 1

def converttodataframe(response):
    lts = []
    if isinstance(response, aconcagua_pb2.GetObservationsResponse):
        for ts in response.seriesdata:
            dts = {'source': ts.key.sourcename, 'series': ts.key.seriesname }
            dts.update(dict(ts.values))
            lts.append(dts)
    
    if isinstance(response, aconcagua_pb2.GetMetadataResponse):
        for ts in response.seriesdata:
            dts = {'source': ts.key.sourcename, 'series': ts.key.seriesname }
            for m, v in zip(response.metadataheaders, ts.values):
                dts.update({m:v})
            lts.append(dts)

    df = pd.DataFrame(lts)
    df.reindex(sorted(df.columns), axis=1)
    return df

def run():

    channel = grpc.insecure_channel('localhost:50051')
    client = aconcagua_pb2_grpc.TimeseriesDataServiceStub(channel)

    response = client.GetVersion(googleEmpty.Empty())    
    print(response)

    source = 'dmx:.\\..\\..\\..\\..\\data\\sample.dmx'
    seriesList = ['911BE','911BEA','BCA_GDP']
    headerList  = ['scale','unit','description']
    frequencyList = 'MA'

    source = 'ecos:\\ECDATA_CPI'
    seriesList = ['312PCPIFBT_IX.A', '612PCPIFBT_IX.M', '612PCPIFBT_IX.Q']
    headerList  = ['SCALE', 'INDICATOR', 'COUNTRY']
    frequencyList = 'MA'

    request = createobservationrequest(
        source,
        seriesList, 
        frequencyList)
    response = client.GetObservations(request)
    showobservationsresponse(response)    
    dfts = converttodataframe(response)

    request = createmetadatarequest(
        source,
        seriesList, 
        headerList)
    response = client.GetMetadata(request)
    showmetadataresponse(response)
    dfts = converttodataframe(response)


if __name__ == '__main__':
    run()

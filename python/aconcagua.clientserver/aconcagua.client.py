
from __future__ import print_function

import grpc
import aconcagua_pb2
import aconcagua_pb2_grpc
from google.protobuf.internal import api_implementation
import pandas as pd

if api_implementation.Type() == 'cpp':
    from google.protobuf.pyext.cpp_message import GeneratedProtocolMessageType
    from google.protobuf.pyext._message import ScalarMapContainer as ScalarMap
    from google.protobuf.pyext._message import RepeatedScalarContainer as RepeatedScalarFieldContainer
    from google.protobuf.pyext._message import RepeatedCompositeContainer as RepeatedCompositeFieldContainer
else:
    from google.protobuf.internal.python_message import GeneratedProtocolMessageType
    from google.protobuf.internal.containers import ScalarMap, RepeatedScalarFieldContainer, RepeatedCompositeFieldContainer


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
    for ts in response.datalist:
        print('[%02d] source[series]: %s[%s]' % (i, ts.key.sourcename, ts.key.seriesname))

        for d in ts.data:
            print('    Data: %s' % d)
        i = i + 1

def showobservationsresponse(response):
    i = 0
    for ts in response.datalist:
        print('[%02d] source[series]: %s[%s]' % (i, ts.key.sourcename, ts.key.seriesname))

        for key in ts.values:
            print('    %s: %d' % (key, ts.values[key]))
        i = i + 1

def converttodataframe(response):
    lts = []
    if isinstance(response, aconcagua_pb2.GetObservationsResponse):
        for ts in response.datalist:
            dts = {'source': ts.key.sourcename, 'series': ts.key.seriesname }
            dts.update(dict(ts.values))
            lts.append(dts)
    
    if isinstance(response, aconcagua_pb2.GetMetadataResponse):
        for ts in response.datalist:
            dts = {'source': ts.key.sourcename, 'series': ts.key.seriesname }
            for m, v in zip(response.metadataheaders, ts.data):
                dts.update({m:v})
            lts.append(dts)

    df = pd.DataFrame(lts)
    df.reindex(sorted(df.columns), axis=1)
    return df

def run():
    seriesList = ['911BE','911BEA','BCA_GDP']
    headerList  = ['scale','unit','description']
    frequencyList = 'MA'

    channel = grpc.insecure_channel('localhost:50051')
    client = aconcagua_pb2_grpc.AconcaguaStub(channel)
    
    request = createobservationrequest(
        'dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx',
        seriesList, 
        frequencyList)
    response = client.GetObservations(request)
    showobservationsresponse(response)    
    dfts = converttodataframe(response)

    request = createmetadatarequest(
        'dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx',
        seriesList, 
        headerList)
    response = client.GetMetadata(request)
    showmetadataresponse(response)
    dfts = converttodataframe(response)


if __name__ == '__main__':
    run()

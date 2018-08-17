
from __future__ import print_function

import grpc
import aconcagua_pb2
import aconcagua_pb2_grpc

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
        print('Sourcename[%02d]: %s/%s' % (i, ts.key.sourcename, ts.key.seriesname))

        for d in ts.data:
            print('    Data: %s' % d)
        i = i + 1

def showobservationsresponse(response):
    i = 0
    for ts in response.datalist:
        print('Sourcename[%02d]: %s/%s' % (i, ts.key.sourcename, ts.key.seriesname))

        for key in ts.values:
            print('    %s: %d' % (key, ts.values[key]))
        i = i + 1

def run():
    seriesList = ['911BE','911BEA','BCA_GDP']
    headerList  = ['scale','unit','description']
    frequencyList = 'MA'

    channel = grpc.insecure_channel('localhost:50051')
    client = aconcagua_pb2_grpc.AconcaguaStub(channel)
    
    request = createmetadatarequest(
        'dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx',
        seriesList, 
        headerList)
    response = client.GetMetadata(request)
    showmetadataresponse(response)

    request = createobservationrequest(
        'dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx',
        seriesList, 
        frequencyList)
    response = client.GetObservations(request)
    showobservationsresponse(response)    


if __name__ == '__main__':
    run()

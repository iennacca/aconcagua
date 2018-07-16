
from __future__ import print_function

import grpc
import aconcagua_pb2
import aconcagua_pb2_grpc

def createmetadatarequest():
    request = aconcagua_pb2.GetMetadataRequest()
    request.requestmetadata['version'] = '0.9'
    request.metadataheaders.extend(['scale','unit'])
    
    # TODO [jc]: find better way of projecting onto the list 
    ssk = aconcagua_pb2.SourceSeriesKey(sourcename = 'null://test')
    for i in range(1,5):
        ssk.seriesname = "series%02d" % i
        request.keys.extend([ssk])

    return request

def showmetadataresponse(response):
    i = 0
    for ts in response.datalist:
        print('Sourcename[%02d]: %s/%s' % (i, ts.key.sourcename, ts.key.seriesname))

        for d in ts.data:
            print('    Data: %s' % d)
        i = i + 1

def run():
    channel = grpc.insecure_channel('localhost:50051')
    client = aconcagua_pb2_grpc.AconcaguaStub(channel)
    request = createmetadatarequest()
    response = client.GetMetadata(request)
    showmetadataresponse(response)

if __name__ == '__main__':
    run()

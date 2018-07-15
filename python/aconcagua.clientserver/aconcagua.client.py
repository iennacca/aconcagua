
from __future__ import print_function

import grpc
import aconcagua_pb2
import aconcagua_pb2_grpc

def run():
    channel = grpc.insecure_channel('localhost:50051')
    client = aconcagua_pb2_grpc.AconcaguaStub(channel)
    request = createmetadatarequest()
    response = client.GetMetadata(request)
    showmetadataresponse(response)

def createmetadatarequest():
    r = aconcagua_pb2.GetMetadataRequest()
    r.metadataheaders.extend(['scale','unit'])
    r.requestmetadata['version'] = '0.9'
    
    # TODO [jc]: find better way of projecting onto the list 
    ssk = aconcagua_pb2.SourceSeriesKey(sourcename = 'null://test')
    for i in range(1,3):
        ssk.seriesname = "series%02d" % i
        r.keys.extend([ssk])

    return r

def showmetadataresponse(response):
    i = 0
    for ts in response.datalist:
        print("Sourcename: %s" + ts.key.sourcename)
        i = i + 1
    
if __name__ == '__main__':
    run()

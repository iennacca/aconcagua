
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
    return aconcagua_pb2.GetMetadataRequest()
    
def showmetadataresponse(response):
    i = 0
    for ts in response.datalist:
        print("Sourcename:" + ts.Key.Soucename)
        i = i + 1
    
    return NotImplemented

if __name__ == '__main__':
    run()

import io
import socket
import numpy as np
from PIL import Image



def sendfile(ipAddress = "127.0.0.1", port = 12345,filePath = "./img.png"):

    client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client.connect((ipAddress, port))

    img = b''
    with open(filePath, 'rb') as f:
        chunk_size = 32768
        while True:
            print ("sent chunk")
            chunk = f.read(chunk_size)
            img += chunk
            if not chunk:
                break

    length = np.int32(len(img)).tobytes()
    data = length + img
    client.send(data)
    client.close()

sendfile(ipAddress = "127.0.0.1", port = 54321  , filePath = "./img.png")
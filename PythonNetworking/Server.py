import io
import socket
from PIL import Image
from NewClient import sendfile
import os.path

chunkSize = 32768
serverIP = socket.gethostbyname(socket.gethostname())

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((serverIP,12345));

print("now listening on: " + serverIP)

while True:
    server.listen()
    conn,address = server.accept()

    print("Accepted from ip: " + address[0])
    prompt = conn.recv(128)

    print(prompt)

    lengthbytes = conn.recv(4)
    imagelength = int.from_bytes(lengthbytes, byteorder='little')


    print("getting image")
    img_data = b''
    received_bytes = 0
    while received_bytes < imagelength:
        chunk = conn.recv(chunkSize)
        img_data += chunk
        received_bytes += len(chunk)
        print("got chunk")


    image = Image.open(io.BytesIO(img_data))

    image.save("img.png")
    print("ping")

    path = #INSERT STABLE DIFFUSION FUNCTION HERE

    if os.path.isfile(path):
        print(address)
        sendfile(address[0],54321,path)
        print ("pong")

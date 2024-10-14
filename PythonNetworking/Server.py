import io
import socket
from PIL import Image

chunkSize = 32768

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("127.0.0.1",12345))

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
    #image = Image.frombytes("RGB",(512,512),img_data)

    image.save("img.png")
    print("saved image")
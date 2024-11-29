import socket
import sys
from PIL import Image
from StableDiffusion import stableDiffusion,makePipe

# Define constants
RECEIVE_PORT = 12345
SEND_PORT = 54321
CHUNK_SIZE = 32768


def send_image(ip_address, image_path):

    print(ip_address)
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as client:
        client.connect((ip_address, SEND_PORT))

        with open(image_path, 'rb') as f:
            image_data = f.read()

        data_length = len(image_data).to_bytes(4, byteorder='little')
        data = data_length + image_data
        client.sendall(data)


def receive_and_process_image(client_socket):

    print("Accepted connection")

    prompt_bytes = client_socket.recv(2048)


    length_bytes = client_socket.recv(4)
    image_length = int.from_bytes(length_bytes, byteorder='little')

    received_bytes = 0
    image_data = b''
    while received_bytes < image_length:
        chunk = client_socket.recv(CHUNK_SIZE)
        image_data += chunk
        received_bytes += len(chunk)

    with open(f"img.png", 'wb') as f:
        f.write(image_data)
    
    prompt = str(prompt_bytes,"utf-8")
    prompt.rstrip('\x00')
    return prompt



def run_server(server_ip):
    pipe= makePipe()
    
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((server_ip, RECEIVE_PORT))
    server_socket.listen()

    print(f"Server listening on {server_ip}")

    while True:
        conn, address = server_socket.accept()
        prompt = receive_and_process_image(conn)    
        gen = stableDiffusion("img.png",prompt,20,pipe)
        send_image(address[0], gen)


if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python script.py [remote | local]")
        sys.exit(1)

    if sys.argv[1] == "remote":
        server_ip = socket.gethostbyname(socket.gethostname())
    elif sys.argv[1] == "local":
        server_ip = "127.0.0.1"
    else:
        print(f"Invalid argument: {sys.argv[1]}")
        sys.exit(1)

    run_server(server_ip)
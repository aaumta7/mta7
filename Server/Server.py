import socket

def receive_file(filename):
    # Create a socket object
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Get local machine name and bind to a port
    host = socket.gethostbyname(socket.gethostname())  # Server will run on localhost
    port = 12345  # Any free port
    server_socket.bind((host, port))

    # Start listening for incoming connections (up to 1 connection)
    server_socket.listen(1)
    print(f"Server listening on {host}:{port}")

    # Accept a connection from a client
    client_socket, addr = server_socket.accept()
    print(f"Got a connection from {addr}")

    # Open a file in binary write mode to save the received data
    with open(filename, 'wb') as file:
        while True:
            # Receive data in chunks
            chunk = client_socket.recv(1024)
            if not chunk:
                break
            file.write(chunk)
        print(f"File received and saved as {filename}")

    # Close the connection
    client_socket.close()
    server_socket.close()

if __name__ == "__main__":
    receive_file('received_beer.jpg')  # File to save on the server

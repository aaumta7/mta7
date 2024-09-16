import socket

def send_file(filename):
    # Create a socket object
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Define the server address and port
    host = '127.0.0.1'  # Server's IP address
    port = 12345  # Server's port
    client_socket.connect((host, port))

    # Open the file in binary mode and send it
    with open(filename, 'rb') as file:
        # Read and send the file in chunks
        chunk = file.read(1024)
        while chunk:
            client_socket.send(chunk)
            chunk = file.read(1024)
        print("File sent successfully")

    # Close the connection
    client_socket.close()

if __name__ == "__main__":
    send_file('beer.jpg')  # File to send to the server

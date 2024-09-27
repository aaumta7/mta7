import socket
import os

def receive_file(save_directory="received_files", port=9000):
    # Ensure the save directory exists
    if not os.path.exists(save_directory):
        os.makedirs(save_directory)

    # Create a TCP/IP socket
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
        # Bind the socket to the address and port
        server_socket.bind((socket.gethostname(), port))
        server_socket.listen(1)
        print(f"Server listening on {socket.gethostname()}:{port}")

        while True:
            # Wait for a connection
            print("Waiting for a connection...")
            connection, client_address = server_socket.accept()

            with connection:
                print(f"Connection from {client_address}")

                # Receive the file name length (4 bytes for the length)
                file_name_length_bytes = connection.recv(4)
                file_name_length = int.from_bytes(file_name_length_bytes, byteorder='little')
                print(f"File name length received: {file_name_length} bytes")

                # Receive the file name
                file_name_bytes = connection.recv(file_name_length)
                file_name = file_name_bytes.decode('utf-8')
                print(f"Receiving file: {file_name}")

                # Prepare the full path to save the file
                file_path = os.path.join(save_directory, file_name)

                # Receive the file data
                with open(file_path, 'wb') as file:
                    print(f"Saving to: {file_path}")
                    while True:
                        data = connection.recv(4096)
                        if not data:
                            break
                        file.write(data)

                print(f"File {file_name} received successfully and saved to {file_path}")

if __name__ == "__main__":
    receive_file()

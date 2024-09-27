using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class FileTransferClient : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; 
    public int serverPort = 9000;         
    private string filePath = "beer.jpg"; 

    private void Start()
    {
        SendFileToServer(filePath);
    }

    private void SendFileToServer(string filePath)
    {
        try
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);

            Debug.Log($"Sending file: {fileName} ({fileData.Length} bytes)");

            TcpClient client = new TcpClient(serverIP, serverPort);
            NetworkStream stream = client.GetStream();

            // Send the file name first (UTF8 encoded)
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
            byte[] fileNameLength = BitConverter.GetBytes(fileNameBytes.Length);
            stream.Write(fileNameLength, 0, fileNameLength.Length);
            stream.Write(fileNameBytes, 0, fileNameBytes.Length);

            // Send the file data
            stream.Write(fileData, 0, fileData.Length);
            Debug.Log("File sent successfully.");

            // Close everything
            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while sending file: {ex.Message}");
        }
    }
}

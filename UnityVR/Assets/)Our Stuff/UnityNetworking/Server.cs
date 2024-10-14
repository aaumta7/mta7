using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NewServer : MonoBehaviour
{
    bool open = false;
    TcpListener listener;

    public const int port = 54321; // Replace with your desired port number
    IPAddress serverIP = getLocal();
    List<TcpClient> clients = new List<TcpClient>();
    void Start()
    {
        Debug.Log(serverIP.ToString());
        listener = new TcpListener(serverIP,port);
        listener.Start();
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
        if (!open ||true )
        {
            open = true;
            Task.Run(async () =>
            {
                //Debug.Log("started async task");
                if (listener.Pending())
                {
                    Debug.Log("accepeted client");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    handleClient(client);
                }
            });
        }
       

    }

    async void handleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] sizeBytes = new byte[4];
        int bytesRead = await stream.ReadAsync(sizeBytes, 0, sizeBytes.Length);

        int imageLength = BitConverter.ToInt32(sizeBytes, 0);

        byte[] imgData = new byte[imageLength];
        int totalBytesReceived = 0;
        try
        {
            while (totalBytesReceived < imageLength)
            {
                bytesRead = await stream.ReadAsync(imgData, totalBytesReceived, imageLength - totalBytesReceived);
                if (bytesRead == 0)
                {
                    Debug.Log("Connection closed by server");
                    throw new Exception("ERROR");
                }

                totalBytesReceived += bytesRead;
                Debug.Log("Got chunk");
            }
        }
        catch (Exception)
        {
            throw;
        }

        //Debug.Log(imgData);
        clients.Remove(client);
        client.Close();
        File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/ServerTest/img2.png", imgData);
        open = false;
        return;
    }
    static IPAddress getLocal()
    {
        List<IPAddress> locals = new List<IPAddress>();
        foreach (var addr in Dns.GetHostEntry(string.Empty).AddressList)
        {
            if (addr.AddressFamily == AddressFamily.InterNetwork)
                locals.Add(addr);
        }
        return locals[0];
    }
}

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

public class Server : MonoBehaviour
{


    public int port = 54321; // Standard for reciving C#




    TcpListener listener;
    IPAddress serverIP = getLocal();


    void Start()
    {
        //for local testing
        //serverIP = IPAddress.Parse("127.0.0.1");

        Debug.Log(serverIP.ToString());
        listener = new TcpListener(serverIP,port);
        listener.Start();
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
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

        //clients.Remove(client);
        client.Close();

        VariableHandler.largest++;
        File.WriteAllBytes(VariableHandler.imageFolderPath +"/"+ VariableHandler.largest.ToString() + ".png", imgData);
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

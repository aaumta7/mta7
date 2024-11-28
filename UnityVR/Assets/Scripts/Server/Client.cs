using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using UnityEngine;



public class Client : MonoBehaviour
{

    public const int port = 12345; //Standard for sending to Python
    public string serverIP;
    public string prompt = "undead anime girl, zombie, hot, ghoul, sexy";

    // Start is called before the first frame update
    void Start()
    {
        serverIP = File.ReadAllText(VariableHandler.ipFile);
        Debug.Log(serverIP);
        VariableHandler.updateImages();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void sendImage()
    {
        byte[] imgBytes = File.ReadAllBytes(VariableHandler.imageFolderPath+"/img.png");

        TcpClient client = new TcpClient();

        client.Connect(serverIP,port);
        //client.Connect("127.0.0.1", port);
        NetworkStream stream = client.GetStream();

        stream.Write(PadString(prompt, 2048));

        byte[] lengthBytes = BitConverter.GetBytes(imgBytes.Length);
        byte[] dataToSend = lengthBytes.Concat(imgBytes).ToArray();


        stream.Write(dataToSend);

        stream.Close();
    }
    public static byte[] PadString(string text, int len)
    {
        byte[] padded = new byte[len];
        byte[] init = Encoding.UTF8.GetBytes(text);

        Array.Copy(init, 0, padded, 0, init.Length);

        if (init.Length <= len)
        {
            for (int i = init.Length; i < len; i++)
            {
                padded[i] = 0;
            }
        }
        else
        {
            throw new Exception("String too long");
        }
        return padded;
    }
}

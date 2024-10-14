using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;



public class NewClient : MonoBehaviour
{

    public const int port = 12345; //Standard for recieving Python
    public string serverIP = "127.0.0.1";
    public string prompt = "test";

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sendImage()
    {
        byte[] imgBytes = File.ReadAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/ServerTest/img.png");

        TcpClient client = new TcpClient();
        client.Connect(serverIP, port);
        NetworkStream stream = client.GetStream();

        stream.Write(PadString(prompt, 128));

        byte[] lengthBytes = BitConverter.GetBytes(imgBytes.Length);
        byte[] dataToSend = lengthBytes.Concat(imgBytes).ToArray();


        stream.Write(dataToSend);

        stream.Close();
    }
    public static byte[] PadString(string text,int len)
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

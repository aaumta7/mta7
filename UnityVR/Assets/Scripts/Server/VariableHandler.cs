using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Drawing;
using UnityEngine.Rendering;

public static class VariableHandler
{
    public static string folderName = "serverTest";
    public static string imageFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/" + folderName;
    public static string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + folderName;
    public static string ipFile = imageFolderPath + "/ip.txt";
    public static int largest = 0;
    public static void updateImages()
    {
        foreach (string s in Directory.GetFiles(imageFolderPath+"/Images").Select(Path.GetFileNameWithoutExtension).ToList())
        {
            if (int.Parse(s) > largest)
            {
                largest = int.Parse(s);
            }
        }
    }
    public static Texture2D getNewestImage()
    { 

        string path = imageFolderPath + "/Images/" + largest.ToString() + ".png";
        // Load the texture from the specified path
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);


        return tex;
    }
}
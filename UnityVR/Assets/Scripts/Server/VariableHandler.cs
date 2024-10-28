using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class VariableHandler
{
    public static string folderName = "serverTest";
    public static string imageFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/" + folderName;
    public static string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + folderName;
    public static string ipFile = imageFolderPath + "/ip.txt";
    public static int largest = 0;
    public static void updateImages()
    {
        foreach (string s in Directory.GetFiles(imageFolderPath).Select(Path.GetFileNameWithoutExtension).ToList())
        {
            if (int.Parse(s) > largest)
            {
                largest = int.Parse(s);
            }
        }
    }

}

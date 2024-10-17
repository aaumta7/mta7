using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ImageUpdater
{
    public static string folderName = "serverTest";
    public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/" + folderName;
    public static int largest = 0;
    public static void update()
    {
        foreach (string s in Directory.GetFiles(folderPath).Select(Path.GetFileNameWithoutExtension).ToList())
        {
            if (int.Parse(s) > largest)
            {
                largest = int.Parse(s);
            }
        }
    }
}

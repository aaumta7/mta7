using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public TMP_Text input;
    public void onClick()
    {
        using (StreamWriter writer = new StreamWriter(VariableHandler.ipFile))
        {
            writer.Write(input.text);
        }
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        if (!Directory.Exists(VariableHandler.imageFolderPath))
        {
            Directory.CreateDirectory(VariableHandler.imageFolderPath);

        }
        if (!Directory.Exists(VariableHandler.documentsFolderPath))
        {
            Directory.CreateDirectory (VariableHandler.documentsFolderPath);
        }
    }
}

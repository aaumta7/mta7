using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    // Thanks stackoverflow: https://stackoverflow.com/questions/10538425/c-sharp-dictionary-to-csv

    private void OnApplicationQuit()
    {
        string csv = string.Join(Environment.NewLine, VariableHandler.manequinnInteractionData.Select(d => $"{d.Key},{d.Value}"));

        string realTime = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        //realTime = realTime.Replace('/', '-');
        string folder = Application.streamingAssetsPath + "/" + realTime + ".csv";
        System.IO.File.WriteAllText(folder, csv);
    }
}

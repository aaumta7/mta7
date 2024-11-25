using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateCanvas : MonoBehaviour
{

    private void Update()
    {
        float timer = 5;
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            VariableHandler.updateImages();
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = VariableHandler.getNewestImage();
            timer = 0;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateCanvas : MonoBehaviour
{

    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            VariableHandler.updateImages();
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = VariableHandler.getNewestImage(5)[0];
            timer = 0;
        }
    }

}

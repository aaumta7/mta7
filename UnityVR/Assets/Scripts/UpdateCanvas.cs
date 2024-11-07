using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateCanvas : MonoBehaviour
{
    

    public void PaintNewImage(byte[] texture, int w, int h)
    {

        Texture2D tex = new Texture2D(w, h);
        tex.LoadImage(texture);
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = tex;
    }
}

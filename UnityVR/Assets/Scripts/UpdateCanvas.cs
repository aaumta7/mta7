using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateCanvas : MonoBehaviour
{
    public MeshRenderer[] frames;
    public List<Texture2D> finishedPaintings = new List<Texture2D>();
    public bool reverse = true;
    float timer = 5;

    private void Start()
    {
        PaintImage();
    }
    void Update(){
        timer += Time.deltaTime;
        if (timer >=5)
        {
            VariableHandler.updateImages();

            // Parameter should be -1 from amount of frames
            finishedPaintings = VariableHandler.getNewestImage(10);

            if (reverse)
            {
                finishedPaintings.Reverse();
            }
            for (int i = 0; i < finishedPaintings.Count; i++)
            {
                frames[i].material.mainTexture = finishedPaintings[i];
            }
            timer = 0;
        }

    }

    public void PaintImage()
    {

        UpdateCanvases();
    }

    private void UpdateCanvases ()
    {

    }
}

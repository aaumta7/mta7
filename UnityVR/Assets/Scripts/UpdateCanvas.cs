using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateCanvas : MonoBehaviour
{
    public MeshRenderer[] frames;
    public List<Texture2D> finishedPaintings = new List<Texture2D>();

    private void Start()
    {
        PaintImage();
    }

    public void PaintImage()
    {
        VariableHandler.updateImages();
        finishedPaintings = VariableHandler.getNewestImage(5);
        finishedPaintings.Reverse();
        UpdateCanvases();
    }

    private void UpdateCanvases ()
    {
        for (int i = 0; i < finishedPaintings.Count; i++)
        {
            frames[i].material.mainTexture = finishedPaintings[i];
        }
    }
}

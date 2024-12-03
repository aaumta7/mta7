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

    public GameObject soundPrefab;
    public AudioClip dingDing;
    int paintingsCount = 0;
    int oldPaintCount = 0;

    private void Start()
    {
        PaintImage();
        VariableHandler.updateImages();
        finishedPaintings = VariableHandler.getNewestImage(10);
        paintingsCount = finishedPaintings.Count;
        oldPaintCount = paintingsCount;
        Debug.Log("New Count: " + paintingsCount + "\nOld Count: " + oldPaintCount);
    }
    void Update(){
        timer += Time.deltaTime;
        if (timer >=5)
        {
            VariableHandler.updateImages();

            // Parameter should be -1 from amount of frames
            finishedPaintings = VariableHandler.getNewestImage(10);
            paintingsCount = finishedPaintings.Count;

            if (reverse)
            {
                finishedPaintings.Reverse();
            }
            for (int i = 0; i < finishedPaintings.Count; i++)
            {
                frames[i].material.mainTexture = finishedPaintings[i];
            }
            timer = 0;
            NewPaintingReadySound();
        }

    }

    void NewPaintingReadySound ()
    {
        if (oldPaintCount == paintingsCount) { return; }
        //if (Time.time < 1f) { return; }
        GameObject sfx = Instantiate(soundPrefab, new Vector3(-0.75f, 3f, 5f), Quaternion.identity);
        sfx.GetComponent<SFXPlayer>().PlaySound(dingDing);
        oldPaintCount = paintingsCount;
    }

    public void PaintImage()
    {

        UpdateCanvases();
    }

    private void UpdateCanvases ()
    {

    }
}

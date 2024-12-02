using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class CamCopy : MonoBehaviour
{
    public Camera snapCam;
    private DrawSkeleton drawSkel;

    int resWidth = 512;//256;
    int resHeight = 512;//256;
    private void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    public void callTakeSnapshot()
    {
        snapCam.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        if (snapCam.gameObject.activeInHierarchy)
        {
            drawSkel = FindObjectOfType<DrawSkeleton>();
            drawSkel.AssembleSkeleton();
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();

            drawSkel.DisassembleSkeleton();
            System.IO.File.WriteAllBytes(VariableHandler.imageFolderPath + "/img.png", bytes);
            GameObject.FindObjectOfType<EmptyUI>().SetNewImage(bytes, resWidth, resHeight);

            snapCam.gameObject.SetActive(false);
        }
    }
}

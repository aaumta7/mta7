using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetViewcamSnapshot : MonoBehaviour
{
    public Camera viewCam;

    public void GetSnapshot()
    {
        var currentRT = RenderTexture.active;
        RenderTexture.active = viewCam.targetTexture;
        viewCam.Render();

        int w = viewCam.targetTexture.width;
        int h = viewCam.targetTexture.height;

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(w, h, UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
        image.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        image.Apply();

        GameObject.FindObjectOfType<UIMethods>().DisplayNewPicture(image);

        RenderTexture.active = currentRT;
    }
}

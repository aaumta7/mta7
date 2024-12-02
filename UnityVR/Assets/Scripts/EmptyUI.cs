using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.IO;
using System.Resources;

public class EmptyUI : MonoBehaviour
{
    public Image lastImage;
    byte[] bytes;

    public void SetNewImage(byte[] lateByte, int resW, int resH)
    {
        bytes = lateByte;

        byte[] imgBytes = File.ReadAllBytes(VariableHandler.imageFolderPath + "/img.png");
        Texture2D texture = new Texture2D(resW, resH, TextureFormat.RGB24, false);
        texture.LoadImage(imgBytes);
        texture.Apply();

        lastImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    public void UploadImageToFolder ()
    {

        VariableHandler.largest++;
        File.WriteAllBytes(VariableHandler.imageFolderPath + "/Images/" + VariableHandler.largest.ToString() + ".png", bytes);

        GameObject.FindObjectOfType<UpdateCanvas>().PaintImage();
    }
}

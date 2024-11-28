using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutEvent : MonoBehaviour
{
    public Material mat;
    float target = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_Brightness", 1.0f);
    }

    private void Update()
    {
        float cur = mat.GetFloat("_Brightness");

        float setBright = Mathf.Clamp(Mathf.Lerp(cur, target, 0.1f), 0f, 1f);
        mat.SetFloat("_Brightness", setBright);
    }

    public void DoFade (float seconds)
    {
        mat.SetFloat("_Brightness", 1.0f);
        StartCoroutine(FadeInOut(seconds));
    }
    
    IEnumerator FadeInOut(float waitSeconds)
    {
        target = 0.001f;
        yield return new WaitForSeconds(waitSeconds);
        mat.SetFloat("_Brightness", 0.0f);

        target = 1.0f;
        yield return new WaitForSeconds(1);
        mat.SetFloat("_Brightness", 1.0f);
    }
}

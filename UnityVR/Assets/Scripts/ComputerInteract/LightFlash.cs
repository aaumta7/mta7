using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    float intensity;
    Light flashy;
    public float speed = 1f;

    private void Start()
    {
        flashy = GetComponent<Light>();
        intensity = flashy.intensity;
    }

    private void Update()
    {
        flashy.intensity = Mathf.Abs(Mathf.Sin(Time.time * speed) * intensity);
    }
}

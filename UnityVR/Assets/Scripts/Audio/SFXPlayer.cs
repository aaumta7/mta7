using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound (AudioClip sound)
    {
        source.clip = sound;
        source.Play();
        Destroy(gameObject, sound.length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RSpawner : MonoBehaviour
{
    public GameObject RagdollPrefab;
    private GameObject thisDoll;

    private void Start()
    {
        RespawnDoll();
    }

    public void RespawnDoll ()
    {
        if (RagdollPrefab != null) {
            Destroy(thisDoll);
            thisDoll = Instantiate(RagdollPrefab, transform.position, Quaternion.identity);
        }
    }

    public void GrabActivated(ActivateEventArgs args)
    {
        RespawnDoll();
    }
}

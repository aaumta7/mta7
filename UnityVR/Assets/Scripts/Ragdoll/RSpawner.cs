using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

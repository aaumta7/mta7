using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCamera : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Camera.gameObject.SetActive(true);
    }
}

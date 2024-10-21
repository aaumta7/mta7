using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CamInteraction : MonoBehaviour
{
    public HandleCamera handleCamera;
    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(TriggerCamera);
    }

    public void TriggerCamera(ActivateEventArgs args)
    {
        handleCamera.Activate();
    }
}

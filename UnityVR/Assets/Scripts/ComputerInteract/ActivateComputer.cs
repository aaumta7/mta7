using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateComputer : MonoBehaviour
{
    public GameObject computerUI;


    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ActivateComputerUI);
    }

    public void ActivateComputerUI(ActivateEventArgs args)
    {
        if (computerUI != null)
        {
            computerUI.SetActive(!computerUI.activeSelf);
        }
    }
}

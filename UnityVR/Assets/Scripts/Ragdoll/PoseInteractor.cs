using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoseInteractor : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public bool kinematicRotation = false;
    public MeshRenderer limbVis;
    Color hoverColor = new Color(0.745f, 0.917f, 1.0f); // Light blue hover color

    private void Start()
    {

        Rigidbody[] childRBs = gameObject.GetComponentsInChildren<Rigidbody>();
        for (int i = 0;  i < childRBs.Length; i++)
        {
            rigidbodies.Add(childRBs[i]);
        }

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(EnablePoseChildren);
        grabbable.selectExited.AddListener(DisablePoseChildren);
        grabbable.hoverEntered.AddListener(OnHoverLimb);
        grabbable.hoverExited.AddListener(OnHoverLimbExit);
    }

    public void EnablePoseChildren(SelectEnterEventArgs args)
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].constraints = RigidbodyConstraints.None;
            if (kinematicRotation == false) { return; }

            if (rigidbodies[i] != this.GetComponent<Rigidbody>())
            {
                rigidbodies[i].isKinematic = true;
            }
        }
    }

    public void DisablePoseChildren(SelectExitEventArgs args)
    {
        StartCoroutine("WaitForSpring");
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].constraints = RigidbodyConstraints.FreezeRotation;
            if (kinematicRotation == false) { return; }

            if (rigidbodies[i] != this.GetComponent<Rigidbody>())
            {
                rigidbodies[i].isKinematic = false;
            }
        }
    }

    public void OnHoverLimb(HoverEnterEventArgs args)
    {
        if (limbVis == null) { return; }
        limbVis.material.color = hoverColor;
    }

    public void OnHoverLimbExit(HoverExitEventArgs args)
    {
        if (limbVis == null) { return; }
        limbVis.material.color = Color.white;
    }

    IEnumerable WaitForSpring()
    {
        yield return null;
    }
}
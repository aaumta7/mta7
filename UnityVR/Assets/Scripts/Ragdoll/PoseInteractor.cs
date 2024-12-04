using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoseInteractor : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public bool kinematicRotation = false;
    public MeshRenderer limbVis;
    Color hoverColor = new Color(0.745f, 0.917f, 1.0f); // Light blue hover color

    bool countGrabTime = false;
    float grabTime = 0f;

    private void Start()
    {
        if (VariableHandler.manequinnInteractionData.ContainsKey(this.transform.name) == false)
        {
            VariableHandler.manequinnInteractionData.Add(this.transform.name, grabTime);
        }
        
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

    private void Update()
    {
        if (countGrabTime)
        {
            grabTime += Time.deltaTime;
        }
    }

    public void EnablePoseChildren(SelectEnterEventArgs args)
    {
        countGrabTime = true;

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
        VariableHandler.manequinnInteractionData[this.transform.name] += grabTime;
        //Debug.Log(this.transform.name + " total time: " + VariableHandler.manequinnInteractionData[this.transform.name]);
        //Debug.Log(this.transform.name + " added time: " + grabTime);

        countGrabTime = false;
        grabTime = 0f;
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
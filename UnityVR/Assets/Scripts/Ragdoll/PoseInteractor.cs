using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoseInteractor : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public bool kinematicRotation = false;

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

    IEnumerable WaitForSpring()
    {
        yield return null;
    }
}
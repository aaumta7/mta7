using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoseInteractor : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    private void Start()
    {
        rigidbodies.Add(gameObject.GetComponent<Rigidbody>());

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(EnablePoseChildren);
        grabbable.selectExited.AddListener(DisablePoseChildren);
    }

    public void EnablePoseChildren(SelectEnterEventArgs args)
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].constraints = RigidbodyConstraints.None;
        }
    }

    public void DisablePoseChildren(SelectExitEventArgs args)
    {
        StartCoroutine("WaitForSpring");
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    IEnumerable WaitForSpring()
    {
        yield return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawSkeleton : MonoBehaviour
{
    public Transform torso;
    public Transform head;
    public Transform rShoulder;
    public Transform rElbow;
    public Transform rHand;
    public Transform lShoulder;
    public Transform lElbow;
    public Transform lHand;
    public Transform rHip;
    public Transform rKnee;
    public Transform rFoot;
    public Transform lHip;
    public Transform lKnee;
    public Transform lFoot;

    public AnimationCurve lineWidth;

    public LineRenderer[] skeleton;

    // Start is called before the first frame update
    void Start()
    {


        LineRenderer lTest = gameObject.AddComponent<LineRenderer>();
        lTest.positionCount = 2;
        lTest.SetPosition(0, torso.position);
        lTest.SetPosition(1, rHip.position);
        skeleton[0] = lTest;

        foreach (LineRenderer l in skeleton)
        {
            l.widthCurve = lineWidth;
            l.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        
    }

    private void ConnectLimb(Transform from, Transform to)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

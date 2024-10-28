using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawSkeleton : MonoBehaviour
{
    public Transform head;
    public Transform torso;
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

    public GameObject skelePoint;

    public AnimationCurve lineWidth;
    public Color[] colors = new Color[18];

    public Transform[] limbs;
    public LineRenderer[] skeleton;
    List<GameObject> skelPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Adjusting settings for Rendering
        foreach (LineRenderer l in skeleton)
        {
            l.widthCurve = lineWidth;
            l.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            l.positionCount = 2;
        }
        ConnectAll();
    }

    private void ConnectAll()
    {
        ConnectLimb(1, 2, 0);
        ConnectLimb(1, 5, 1);
        ConnectLimb(2, 3, 2);
        ConnectLimb(3, 4, 3);
        ConnectLimb(5, 6, 4);
        ConnectLimb(6, 7, 5);
        ConnectLimb(1, 8, 6);
        ConnectLimb(8, 9, 7);
        ConnectLimb(9, 10, 8);
        ConnectLimb(1, 11, 9);
        ConnectLimb(11, 12, 10);
        ConnectLimb(12, 13, 11);
        ConnectLimb(1, 0, 12);
        ConnectLimb(0, 14, 13);
        ConnectLimb(14, 16, 14);
        ConnectLimb(0, 15, 15);
        ConnectLimb(15, 17, 16);

        foreach (Transform t in limbs)
        {
            GameObject point = Instantiate(skelePoint, t.position, Quaternion.identity);
            skelPoints.Add(point);
        }
        Debug.Log(skelPoints.Count);
        for (int i = 0; i < skelPoints.Count; i++)
        {
            skelPoints[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }
    }

    private void ConnectLimb(int from, int to, int i)
    {
        skeleton[i].SetPosition(0, limbs[from].position);
        skeleton[i].SetPosition(1, limbs[to].position);
        Gradient gradient = new Gradient();
        gradient.SetKeys(
        new GradientColorKey[] { new GradientColorKey(colors[i], 0.0f), new GradientColorKey(colors[i], 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(.6f, 0.0f), new GradientAlphaKey(.6f, 1.0f) }
        );

        skeleton[i].colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

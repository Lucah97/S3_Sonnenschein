using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CanonTrajectory : MonoBehaviour {

    [Header("Trajectory Contruction")]
    public int segAmount = 25;
    public float segScale = 1f;
    public float strength = 5f;
    public float gravityMult = 1f;

    [Header("Appearance")]
    public Color startColor;
    [Range(0, 1f)]
    public float startTranslucency = 1f;
    public Color endColor;
    [Range(0, 1f)]
    public float endTranslucency = 0.5f;

    private Collider hitObj;
    private GameObject model;
    private int collisionIndex;

    //### Custom Functions ###
    public void updateTrajectory()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        model = transform.GetChild(0).gameObject;

        Vector3[] seg = new Vector3[segAmount+1];
        seg[0] = transform.position;
        Vector3 segVel = model.transform.up * strength;
        hitObj = null;
        collisionIndex = segAmount;

        for (int i = 1; i < segAmount; i++)
        {
            float segTime = (segVel.sqrMagnitude != 0) ? segScale / segVel.magnitude : 0;
            segVel += ((Physics.gravity * gravityMult) * segTime);
            segVel.z = 0;

            //Check for collision
             RaycastHit hit;
            if (Physics.Raycast(seg[i-1], segVel, out hit, segScale*1.5f))
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CamZone"))
                {
                    hitObj = hit.collider;

                    seg[i] = seg[i - 1] + (segVel.normalized * hit.distance);
                    seg[i + 1] = seg[i];
                    segVel = segVel - Physics.gravity * (segScale - hit.distance) / segVel.magnitude;
                    segVel = Vector3.zero;

                    collisionIndex = i + 1;
                    break;
                }
                else
                {
                    seg[i] = seg[i - 1] + segVel * segTime;
                }
            }
            else
            {
                seg[i] = seg[i - 1] + segVel * segTime;
            }
        }

        //Setup Line Renderer
        Color nsCol = startColor;
        Color neCol = endColor;

        Gradient lrGrad = new Gradient();
        lrGrad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(nsCol, 0.0f), new GradientColorKey(neCol, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(startTranslucency, 0.0f), new GradientAlphaKey(endTranslucency, 1.0f) }
            );
        lr.colorGradient = lrGrad;

        lr.SetVertexCount(collisionIndex);
        for (int i=0; i<collisionIndex; i++)
        {
            lr.SetPosition(i, seg[i]);
        }

        //Override CameraPosition
        CameraMovement.instance.setOverridePosition(true, seg[collisionIndex]);
    }
}

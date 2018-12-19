using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(LineRenderer))]

public class VisionConeDetection : MonoBehaviour
{
    private Transform player = null;
    private Transform originPoint;
    private LineRenderer lineRen;

    private float detectionProgress = 0f;
    private Vector3[] lastPoints;

    [Header("Detection Properties")]
    [Range(0f, 100f)]
    public float detectionSpeed = 1f;

    [Header("Line Properties")]
    public float lineWidth = 0.2f;
    public Color lineColor = Color.red;

    //### Built-In Functions ###
    void Start()
    {
        originPoint = transform.GetChild(0);
        lineRen = GetComponent<LineRenderer>();
        setupLineRenderer(ref lineRen);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            detectionProgress += (detectionSpeed * Time.deltaTime);
            detectionProgress = Mathf.Clamp(detectionProgress, 0f, 100f);

            setLinePoints(ref lineRen);
            setAnimatorDistance(lastPoints);
        }
        else
        {
            setAnimatorDistance(null);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject.transform;

            detectionProgress = 0f;
            setLinePoints(ref lineRen);
            lineRen.enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {    
            lineRen.enabled = false;
            player = null;
        }
    }

    //### Custom Functions ###
    private void setupLineRenderer(ref LineRenderer l)
    {
        //Line Width
        l.startWidth = lineWidth;
        l.endWidth = lineWidth;

        //Line Material
        Material lineMat = new Material(Shader.Find("Unlit/Color"));
        lineMat.color = lineColor;
        l.material = lineMat;
    }

    private void setLinePoints(ref LineRenderer l)
    {
        Vector3[] linePoints = new Vector3[2];
        linePoints[0] = originPoint.position;
        linePoints[1] = GetComponent<Collider>().ClosestPoint(player.transform.position);

        //Progress Vector
        Vector3 dir = (linePoints[0] - linePoints[1]).normalized;
        float dist = Vector3.Distance(linePoints[0], linePoints[1]);
        linePoints[0] = linePoints[1] + (dir * (dist * (detectionProgress / 100f)));

        l.SetPositions(linePoints);

        //Save last points
        lastPoints = linePoints;
    }

    private void setAnimatorDistance(Vector3[] points)
    {
        Animator anim = transform.parent.parent.GetComponent<Animator>();
        if (points != null)
        {
            anim.SetFloat("ConeDistance", Vector3.Distance(points[0], points[1]));
        }
        else
        {
            anim.SetFloat("ConeDistance", 99f);
        }
    }
}

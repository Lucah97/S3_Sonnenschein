using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(LineRenderer))]

public class VisionConeDetection : MonoBehaviour
{
    private Transform followObj = null;
    private Transform legs = null;
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
        if (followObj != null)
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
        if ((col.gameObject.tag == "Player"))
        {
            if (!col.GetComponent<PlayerAbilities>().isHidden())
            {
                followObj = col.gameObject.transform;

                detectionProgress = 0f;
                setLinePoints(ref lineRen);
                lineRen.enabled = true;
            }
        }

        if ((col.gameObject.tag == "Legs"))
        {
            followObj = col.gameObject.transform;

            detectionProgress = 0f;
            setLinePoints(ref lineRen);
            lineRen.enabled = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player") || (col.gameObject.tag == "Legs"))
        {    
            lineRen.enabled = false;
            //followObj = null;
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
        linePoints[1] = GetComponent<Collider>().ClosestPoint(followObj.transform.position);

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
        Animator anim = transform.parent.GetComponent<Animator>();
        //Debug.Log(anim.gameObject.name);
        if (points != null)
        {
            float mult = 1f;
            if (followObj.CompareTag("Legs"))
            {
                mult = 3f;
            }
            anim.SetFloat("ConeDistance", Vector3.Distance(points[0], points[1]) * mult);
            anim.SetInteger("chaseID", followObj.GetInstanceID());
        }
        else
        {
            anim.SetFloat("ConeDistance", 99f);
        }
    }

    public Transform getFollowObj()
    {
        if (followObj != null)
        {
            Debug.Log("First one");
            return followObj;
        }
        else
        {
            Debug.Log("Second one");
            return GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }

    public void folInfo()
    {
        try { Debug.Log(followObj.name); }
        catch { Debug.Log("NONE"); }
        
    }
}

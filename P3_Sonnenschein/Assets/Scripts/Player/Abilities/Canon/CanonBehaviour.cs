using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanonTrajectory))]
[RequireComponent(typeof(LineRenderer))]
public class CanonBehaviour : MonoBehaviour {

    public bool allowInput = true;

    [Header("Rotation Properties")]
    public float maxRotation;
    public float rotationSpeed;
    private float curRotation;

    private CanonTrajectory ct;
    private LineRenderer lr;

    //### Built-In Functions ###
    void Start () {
        ct = GetComponent<CanonTrajectory>();
        lr = GetComponent<LineRenderer>();

        //Reset rotation variables
        transform.rotation = Quaternion.Euler(Vector3.zero);
        curRotation = 0;
	}

	void Update () {
		if (allowInput) { processInput(); }
        applyRotation();
	}

    //### Custom Functions ###
    private void processInput()
    {
        //Horizontal Input for rotation
        float horInput = Input.GetAxis("Horizontal");
        curRotation -= (horInput * rotationSpeed * Time.deltaTime);
        curRotation = Mathf.Clamp(curRotation, -maxRotation, maxRotation);

        //Launch Canon
    }

    private void applyRotation()
    {
        Vector3 desiredEulers = new Vector3(0,0,curRotation);
        transform.rotation = Quaternion.Euler(desiredEulers);
    }

    private void applyTrajectory()
    {
        lr = ct.getTrajectory();
    }
}

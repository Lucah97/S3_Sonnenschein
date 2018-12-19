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

    [Header("Aim Properties")]
    public float strength;
    public float velocityMult;

    private PlayerMovement pm;
    private CanonTrajectory ct;
    private GameObject model;

    //### Built-In Functions ###
    void Start ()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        ct = GetComponent<CanonTrajectory>();
        model = transform.GetChild(0).gameObject;

        //Reset rotation variables
        model.transform.rotation = Quaternion.Euler(Vector3.zero);
        curRotation = 0;
	}

	void Update ()
    {
		if (allowInput) { processInput(); }

        //Update canon trajectory
        applyRotation();
        ct.updateTrajectory();
	}

    //### Custom Functions ###
    private void processInput()
    {
        //Horizontal Input for rotation
        float horInput = Input.GetAxis("Horizontal");
        curRotation -= (horInput * rotationSpeed * Time.deltaTime);
        curRotation = Mathf.Clamp(curRotation, -maxRotation, maxRotation);

        //Launch Canon
        if (Input.GetAxis("Jump") == 1)
        {
            pm.enabled = true;
            pm.setAllowIinput(false);
            pm.setCanonMode(true);
            pm.setVelocity(model.transform.up * (strength * velocityMult));
            pm.transform.parent = null;

            GameObject.Destroy(this.gameObject);
        }
    }

    private void applyRotation()
    {
        Vector3 desiredEulers = new Vector3(0,0,curRotation);
        model.transform.rotation = Quaternion.Euler(desiredEulers);
    }
}

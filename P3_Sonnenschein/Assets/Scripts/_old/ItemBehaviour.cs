using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    public float rotationSpeed = 1;
    public float bounceSpeed = 1;
    public float bounceAmount = 1;

    private Vector3 origPos;
    private float elapsedTime = 0;

    // Use this for initialization
    void Start () {
        origPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 99999)
        {
            elapsedTime = 0;
        }

        //Bounce
        Vector3 desiredPos = origPos;
        desiredPos.y += (Mathf.Sin(elapsedTime * bounceSpeed) * bounceAmount);
        transform.position = desiredPos;

        //Rotation
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

	}
}

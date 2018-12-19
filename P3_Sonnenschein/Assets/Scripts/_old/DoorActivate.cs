using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivate : MonoBehaviour {

    private bool openDoor = false;
    private Transform door;
    private Transform anchor;

    private float elapsedTime = 0;

    public Animator nEnAn;

	// Use this for initialization
	void Start () {
        door = transform.parent;
        anchor = door.GetChild(1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ((openDoor) && (elapsedTime < 1.2f))
        {
            door.RotateAround(anchor.position, new Vector3(0, 1, 0), -70 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
	}
}

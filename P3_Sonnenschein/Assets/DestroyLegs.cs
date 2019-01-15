using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLegs : MonoBehaviour {

    public float radius;
	
	// Update is called once per frame
	void LateUpdate () {
        Collider[] curCol = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in curCol)
        {
            if (c.CompareTag("Legs"))
            {
                //GameObject.Destroy(c.gameObject);
            }

            if (c.CompareTag("Player"))
            {
                //Stuff
            }
        }
	}
}

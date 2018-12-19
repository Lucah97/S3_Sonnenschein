using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTriggerZones : MonoBehaviour {

    public Transform pointIN;
    public Transform pointOUT;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            float curZ = other.transform.position.z;

            if (Mathf.Abs(curZ - pointIN.position.z) < Mathf.Abs(curZ - pointOUT.position.z))
            {
                pm.setZdepth(pointOUT.position.z, false);
            }
            else
            {
                pm.setZdepth(pointIN.position.z, false);
            }
        }
    }
}

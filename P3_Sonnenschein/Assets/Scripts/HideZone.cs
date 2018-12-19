using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class HideZone : MonoBehaviour {

    public Transform hidePosition;
    public Light hideIndicatorLight;
    public float hideAnimSpeed = 1;

    private float hilIntensity;

    //### Built-In Functions ###
    void Start () {
        hilIntensity = hideIndicatorLight.intensity;
        hideIndicatorLight.intensity = 0;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHideMovement>().assignHideZone(this);
            enableIndicatorLight(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHideMovement>().assignHideZone(null);
            enableIndicatorLight(false);
        }
    }

    //### Custom Functions ###
    public void enableIndicatorLight(bool setting)
    {
        hideIndicatorLight.intensity = (setting) ? hilIntensity : 0;
    }

    public Vector3 getHidePosition()
    {
        return hidePosition.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class CameraMovementZone : MonoBehaviour {

    [Header("Camera Properties")]
    public bool followHorizontal;
    public bool followVertical;
    public bool followPlayerVelocity;
    public float playerVelocityMultiplier;
    public Vector2 clipHorizontal;
    public Vector2 clipVertical;
    public Vector2 cameraOffset;
    public float cameraDistance;
    public float cameraSmoothTime;

    [Header("Miscellaneous Properties")]
    public Light[] attachedLights;

    //### Built-In Functions ###
    void OnTriggerEnter(Collider other)
    {
        //Assign this CMZ to the Camera
        if (other.CompareTag("Player"))
        {
            CameraMovement.instance.assignCMZ(this);
        }
    }

    //### Custom Functions ###
    public void setLightsActive(bool state)
    {
        for (int i=0; i<attachedLights.Length; i++)
        {
            attachedLights[i].enabled = state;
        }
    }
}

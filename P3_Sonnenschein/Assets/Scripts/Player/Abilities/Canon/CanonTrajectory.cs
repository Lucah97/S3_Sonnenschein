using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTrajectory : MonoBehaviour {

    public int segAmount = 25;
    public float segScale = 1f;
    private Collider hitObj;

    //### Custom Functions ###
    public LineRenderer getTrajectory()
    {
        //Vector3


        LineRenderer lr = new LineRenderer();
        return lr;
    }
}

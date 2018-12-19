using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour, InterfaceLetherTrigger {

    public Vector3 rotDist = new Vector3(90, 0, 0);
    private Vector3 tempPos;

    public void OnSwitchTrigger()
    {
        Vector3 curEuler = transform.rotation.eulerAngles;
        curEuler += rotDist;
        transform.rotation = Quaternion.Euler(curEuler);

        rotDist *= 1;

        GameObject.Destroy(GameObject.FindGameObjectWithTag("deinemama"));
    }
}

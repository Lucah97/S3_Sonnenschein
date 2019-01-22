using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovePlatform : MonoBehaviour, InterfaceLetherTrigger {

	public Vector3 moveDist = new Vector3(4,0,0);
    public float movementSpeed;
    public float distanceCutOff;

    private Vector3 desPos;
    private Vector3 origPos;

    private void Start()
    {
        origPos = transform.position;
        desPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, desPos) > distanceCutOff)
        {
            transform.position = Vector3.Lerp(transform.position, desPos, Time.deltaTime * movementSpeed);
        }
    }

    public void OnSwitchTrigger()
	{
        desPos = (desPos == origPos) ? origPos + moveDist : origPos;
	}
}

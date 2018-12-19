using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovePlatform : MonoBehaviour, InterfaceLetherTrigger {

	public Vector3 moveDist = new Vector3(4,0,0);
	private Vector3 tempPos;
	
	public void OnSwitchTrigger()
	{
		Debug.Log("I get called");
		tempPos = transform.position;
		tempPos = tempPos + (moveDist);
        transform.position = tempPos;

        moveDist *= -1;
	}
}

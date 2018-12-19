using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotationLerp : MonoBehaviour {

    public Transform[] stations;
    public float lerpSpeed = 2f;
    public float timeUntilMove = 3.5f;

    private float elapsedTime = 0f;
    private int curIndex = 0;

	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Lerp(transform.rotation, stations[curIndex].rotation, Time.deltaTime * lerpSpeed);

        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeUntilMove)
        {
            elapsedTime = 0;
            if (curIndex < (stations.Length-1))
            {
                curIndex++;
            }
            else
            {
                curIndex = 0;
            }
        }
	}
}

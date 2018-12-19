using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSymbolMovement : MonoBehaviour {

    public float verticalOffset = 0.3f;
    public float movementSpeed = 0.2f;
    public float fadeOutSpeed = 1f;

    private Vector3 desiredPosition;
    private Material curMat;
    private Color curColor;

    private void Start()
    {
        desiredPosition = transform.position + new Vector3(0, verticalOffset, 0);
        curMat = GetComponent<Renderer>().material;
        curColor = curMat.color;
    }

    void FixedUpdate ()
    {
        //Lerp to position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * movementSpeed);

        //Lerp Color
        curColor.a = Mathf.Lerp(curColor.a, 0f, Time.deltaTime * fadeOutSpeed);
        curMat.color = curColor;

        //Check deletion
        if (curColor.a <= 0.01f) { Destroy(this.gameObject); }
	}
}

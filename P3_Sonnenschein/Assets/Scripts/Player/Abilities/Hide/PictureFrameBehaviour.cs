using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameBehaviour : MonoBehaviour {

    public float addTiling;
    public float addOffset;

    public float curSpeed;
    public float endSpeed;
    public float speedDecrease;

    public float minTiling;

    private float curTiling;
    private float curOffset;

    private bool hasSpedUp = false;

    private PlayerMovement pm;

    private Material holeMat;

	void Start () {
        holeMat = transform.GetChild(0).GetComponent<Renderer>().material;
        curTiling = holeMat.GetTextureScale("_MainTex").x;
        curOffset = holeMat.GetTextureOffset("_MainTex").x;

        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        //Disable Collider
        pm.GetComponent<Collider>().enabled = false;
        pm.setApplyGravity(false);
    }
	
	void Update () {
        //Calculate speed
        curSpeed = Mathf.Lerp(curSpeed, endSpeed, speedDecrease * Time.deltaTime);

        //Adjust texture tiling and offset
        curTiling += (addTiling * curSpeed * Time.deltaTime);
        curOffset += (addOffset * curSpeed * Time.deltaTime);

        holeMat.SetTextureScale("_MainTex", new Vector2(curTiling, curTiling));
        holeMat.SetTextureOffset("_MainTex", new Vector2(curOffset, curOffset));

        //Check if finished
        if (curTiling < minTiling)
        {
            pm.returnToLastZdepth();
            pm.setAllowIinput(true);

            //Enable Collider
            pm.GetComponent<Collider>().enabled = true;
            pm.setApplyGravity(true);

            Destroy(this.gameObject);
        }

        //Speed up by Jump
        if ((Input.GetAxis("Jump") == 1) && (!hasSpedUp))
        {
            curSpeed *= 1.5f;
            endSpeed *= 1.5f;
            hasSpedUp = true;
        }
        if (Input.GetAxis("Jump") == 0)
        {
            hasSpedUp = false;
        }

    }
}

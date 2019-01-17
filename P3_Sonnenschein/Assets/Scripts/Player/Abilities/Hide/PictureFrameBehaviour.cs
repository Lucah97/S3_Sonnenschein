using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameBehaviour : MonoBehaviour {

    public bool spamDirection = true;
    public float speedLerpSpeed;

    public float addTiling;
    public float addOffset;

    public float curSpeed;
    public float endSpeed;
    private float normCurSpeed;
    private float normEndSpeed;

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

        normCurSpeed = curSpeed;
        normEndSpeed = endSpeed;

        //Disable Collider
        pm.GetComponent<Collider>().enabled = false;
        pm.setApplyGravity(false);
    }
	
	void Update () {
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

        //Adjust speed by jumping
        if ((Input.GetAxis("Jump") == 1) && (!hasSpedUp))
        {
            curSpeed *= (spamDirection ? 1.5f : 0.75f);
            endSpeed *= (spamDirection ? 1f : 1.32f);
            hasSpedUp = true;
        }
        if (Input.GetAxis("Jump") == 0)
        {
            hasSpedUp = false;

            //Calculate speed
            curSpeed = Mathf.Lerp(curSpeed, endSpeed, speedDecrease * Time.deltaTime);

            //Adjust endSpeed
            endSpeed = Mathf.Lerp(endSpeed, (normEndSpeed), (Time.deltaTime * speedDecrease));
        }

    }
}

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

    private Material holeMat;

	void Start () {
        holeMat = transform.GetChild(0).GetComponent<Renderer>().material;
        curTiling = holeMat.GetTextureScale("_MainTex").x;
        curOffset = holeMat.GetTextureOffset("_MainTex").x;
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
            PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            pm.returnToLastZdepth();
            pm.setAllowIinput(true);

            Destroy(this.gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonIndicator : MonoBehaviour, UI_Obj_Interface {

    private Vector3 origRot;
    private Vector3 origCale;
    private Vector3 curOffset;

    private void Start()
    {
        origRot = transform.rotation.eulerAngles;
        origCale = transform.localScale;
    }

    public void setAnchorObject(GameObject obj, Vector3 offset)
    {
        transform.parent = obj.transform;
        transform.localPosition = offset;
        curOffset = offset;
    }

	public void setImg(Texture2D imgTex)
    {
        Material buttonMat = new Material(Shader.Find("Unlit/Transparent Cutout"));
        buttonMat.SetTexture("_MainTex", imgTex);
        transform.GetChild(0).GetComponent<Renderer>().material = buttonMat;
    }

    public void setText(string nText)
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = nText;
        //set size based on length
    }

    public void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(origRot);
        transform.localScale = origCale;
        transform.position = transform.parent.position + curOffset;
    }
}

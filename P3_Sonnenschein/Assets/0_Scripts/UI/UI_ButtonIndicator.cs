using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonIndicator : MonoBehaviour, UI_Obj_Interface {

    public void setAnchorObject(GameObject obj, Vector3 offset)
    {
        transform.parent = obj.transform;
        transform.localPosition = offset;
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
        transform.rotation = Quaternion.Euler(new Vector3(90, -90, 90));
        transform.localScale = new Vector3(0.3330514f, 0.3330514f, 0.1132375f);
    }
}

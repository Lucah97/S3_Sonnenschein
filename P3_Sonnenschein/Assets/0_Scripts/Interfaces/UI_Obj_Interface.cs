using UnityEngine;

public interface UI_Obj_Interface {

    void setAnchorObject(GameObject obj, Vector3 offset);
    void setImg(Texture2D imgTex);
    void setText(string nText);

}
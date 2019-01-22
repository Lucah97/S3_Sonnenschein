using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Spawner : MonoBehaviour
{

    public static UI_Spawner instance;

    public GameObject[] UI_Prefabs;
    public Texture2D[] ButtonGraphics;

    private List<GameObject> spawnedElements;

    private void Awake()
    {
        //Singleton Instance
        if ((instance != this) && (instance != null))
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        spawnedElements = new List<GameObject>();
    }

    public GameObject spawn(UI_Types t, Ctrl_Buttons b, string text, GameObject anchor, Vector3 offset)
    {
        GameObject UIobj = new GameObject();
        Texture2D curButton;

        UIobj = Instantiate(UI_Prefabs[(int)t]);
        curButton = ButtonGraphics[(int)b];

        UIobj.GetComponent<UI_Obj_Interface>().setText(text);
        UIobj.GetComponent<UI_Obj_Interface>().setImg(curButton);
        UIobj.GetComponent<UI_Obj_Interface>().setAnchorObject(anchor, offset);

        spawnedElements.Add(UIobj);

        return UIobj;
    }
}

public enum Ctrl_Buttons
{
    A, B, X, Y, L, R, LT, RT
};

public enum UI_Types
{
    ButtonIndicator,
    TextBox
};

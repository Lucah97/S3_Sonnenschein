using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePainting : MonoBehaviour {

    public GameObject target;

    private bool paintingIsPlaced = false;
    private Renderer paintingRenderer;
    private GameObject player;

	void Start () {
        paintingRenderer = transform.GetChild(0).GetComponent<Renderer>();
        paintingRenderer.enabled = false;
	}
	
	void Update () {
		if (player != null)
        {
            if ((Input.GetKeyDown(KeyCode.X)) && (!paintingIsPlaced))
            {
                paintingIsPlaced = true;
                paintingRenderer.enabled = true;
                target.GetComponent<InterfaceLetherTrigger>().OnSwitchTrigger();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DeineMidda");
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}

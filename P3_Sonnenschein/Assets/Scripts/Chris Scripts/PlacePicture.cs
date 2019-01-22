using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePicture : MonoBehaviour {

    public bool placedPic;

    public GameObject picture;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.W))
        {
            picture.gameObject.SetActive(true);

            placedPic = true;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            /*
            //Disable Scripts
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerHideMovement>().enabled = false;
            GetComponent<Animator>().enabled = false;

            //Look at player
            Vector3 lookPos = player.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            //Show Overlay
            GameObject fi = GameObject.Find("FailureImage");
            fi.GetComponent<Image>().enabled = true;
            */
        }
    }
}

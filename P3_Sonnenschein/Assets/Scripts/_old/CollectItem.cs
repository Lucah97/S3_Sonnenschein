using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // other.GetComponent<PlayerMovementTest>().hasKey = true;
            Destroy(this.gameObject);
        }
    }
}

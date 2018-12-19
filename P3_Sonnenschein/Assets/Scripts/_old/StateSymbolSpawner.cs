using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class StateSymbolSpawner : MonoBehaviour {

    public GameObject[] symbols;

    public GameObject spawnSymbol(int index)
    {
        float height = GetComponent<CapsuleCollider>().height;
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += (height / 2) * transform.localScale.y;
        return Instantiate(symbols[index], spawnPosition, symbols[index].transform.rotation);
    }
}

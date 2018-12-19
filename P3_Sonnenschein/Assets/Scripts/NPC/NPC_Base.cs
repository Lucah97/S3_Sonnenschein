using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Base : StateMachineBehaviour
{
    public GameObject player;
    public GameObject npc;

    public float movementSpeed;
    public float rotationSpeed;

    //### Built-In Functions ###
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.gameObject;
        animator.SetBool("hasSearched", false);
    }

    //### Custom Functions ###
    public void setValues(float n_Vmov, float n_Vrot, Color n_Color)
    {
        movementSpeed = n_Vmov;
        rotationSpeed = n_Vrot;
        npc.GetComponent<Renderer>().material.color = n_Color;
    }
}

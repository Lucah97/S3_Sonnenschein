using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NPC_Base : StateMachineBehaviour
{
    public GameObject followObj;
    public GameObject npc;
    public GameObject chaseObj;

    public float movementSpeed;
    public float rotationSpeed;

    //### Built-In Functions ###
    public void Awake()
    {
        //
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followObj = animator.transform.GetChild(0).GetComponent<VisionConeDetection>().getFollowObj().gameObject;

        npc = animator.gameObject;

        //Set movement/rotation Speed
        npc.GetComponent<NavMeshAgent>().speed = movementSpeed;
        npc.GetComponent<NavMeshAgent>().angularSpeed = rotationSpeed;


        animator.SetBool("hasSearched", false);
    }

    //### Custom Functions ###
    public void setValues(float n_Vmov, float n_Vrot, Color n_Color)
    {
        //movementSpeed = n_Vmov;
        //rotationSpeed = n_Vrot;
        npc.GetComponent<Renderer>().material.color = n_Color;
    }
}

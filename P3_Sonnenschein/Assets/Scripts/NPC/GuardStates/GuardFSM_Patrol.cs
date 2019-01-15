using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Patrol : NPC_Base {

    public float distanceCutOff = 0.3f;
    public GameObject[] patrolSpots;

    private int curSpot = 0;

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        patrolSpots = animator.GetComponent<GimmeDemPatrolSpots>().patrolSpots;
        setValues(0.19f, 8f, Color.blue);

        //Spawn Symbol
        StateSymbolSpawner spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(2);
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (patrolSpots.Length == 0) return;
        if (Vector3.Distance(patrolSpots[curSpot].transform.position, npc.transform.position) < distanceCutOff)
        {
            curSpot++;
            if (curSpot >= patrolSpots.Length)
            {
                curSpot = 0;
            }
        }
        /*
        Vector3 dir = patrolSpots[curSpot].transform.position - npc.transform.position;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, 
                                                    Quaternion.LookRotation(dir),
                                                    Time.deltaTime * rotationSpeed);

                                 

        npc.transform.Translate(0, 0, Time.deltaTime * movementSpeed);*/
        npc.GetComponent<NavMeshAgent>().SetDestination(patrolSpots[curSpot].transform.position);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}



	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

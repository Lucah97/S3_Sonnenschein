using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Suspicious : NPC_Base {

    private Vector3[] susPoints;
    private int curSpot;
    private StateSymbolSpawner spawner;

    public float accuracy = 2f;
    public float distanceCutOff = 0.3f;

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        setValues(0.23f, 7f, Color.yellow);

        //Spawn Symbol
        spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(0);

        //Creating suspicious points
        susPoints = new Vector3[Random.Range(5, 8)];
        for (int i=0; i<susPoints.Length; i++)
        {
            susPoints[i] = player.transform.position;
            susPoints[i].y = npc.transform.position.y;
            susPoints[i].x += Random.Range(-accuracy, accuracy);
        }
        susPoints[0] = player.transform.position;
        susPoints[0].y = npc.transform.position.y;
        curSpot = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (susPoints.Length == 0) return;
        if (Vector3.Distance(susPoints[curSpot], npc.transform.position) < distanceCutOff)
        {
            curSpot++;
            Debug.Log(curSpot);
            if (curSpot >= susPoints.Length-1)
            {
                animator.SetBool("hasSearched", true);
            }
            else
            {
                spawner.spawnSymbol(0);
            }
        }

        if (curSpot < susPoints.Length)
        {/*
            Vector3 dir = susPoints[curSpot] - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                        Quaternion.LookRotation(dir),
                                                        Time.deltaTime * rotationSpeed);

            npc.transform.Translate(0, 0, Time.deltaTime * movementSpeed);*/
            npc.GetComponent<NavMeshAgent>().SetDestination(susPoints[curSpot]);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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

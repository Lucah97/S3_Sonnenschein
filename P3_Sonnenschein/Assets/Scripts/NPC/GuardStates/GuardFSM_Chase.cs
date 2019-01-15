using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Chase : NPC_Base {

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        setValues(0.55f, 7f, Color.red);

        //Spawn Symbol
        StateSymbolSpawner spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(1);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        Vector3 aimPos = player.transform.position;
        aimPos.y = npc.transform.position.y;
        Vector3 dir = aimPos - npc.transform.position;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    Time.deltaTime * rotationSpeed);

        npc.transform.Translate(0, 0, Time.deltaTime * movementSpeed);*/
        npc.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
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

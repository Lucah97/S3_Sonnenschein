using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyTestBehaviour : MonoBehaviour {

    public Transform[] patrolPoints;
    public int currentGoalPoint = 0;
    public float goalDistanceDelta = 0.05f;
    public float moveSpeed = 1;
    public float rotationSpeed = 1;

    private GameObject Lamp;
    private Rigidbody rb;
    private EnemyStates es = EnemyStates.St_Patroling;

    private void Start()
    {
        Lamp = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
		switch (es)
        {
            case (EnemyStates.St_Patroling):
                rotateTowardsGoal();
                moveTowardsGoal();
                if (checkReachedGoal())
                    nextGoal();
                break;

            case (EnemyStates.St_Chasing):

                break;
        }
	}

    void moveTowardsGoal()
    {
        Vector3 direction = patrolPoints[currentGoalPoint].position - transform.position;
        direction.Normalize();

        rb.AddForce((direction * moveSpeed), ForceMode.Impulse);
    }

    void rotateTowardsGoal()
    {
        Vector3 direction = patrolPoints[currentGoalPoint].position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.cyan);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction.normalized, Time.deltaTime * rotationSpeed, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
    
        Quaternion nRot = Quaternion.LookRotation(newDir);
        Vector3 eRot = nRot.eulerAngles;
        eRot = new Vector3(0, eRot.y, 0);
        transform.rotation = Quaternion.Euler(eRot);
    }

    bool checkReachedGoal()
    {
        float dist = Vector3.Distance(transform.position, patrolPoints[currentGoalPoint].position);

        return (dist < goalDistanceDelta) ? true : false;
    }

    void nextGoal()
    {
        currentGoalPoint = (currentGoalPoint < (patrolPoints.Length - 1)) ? 1 : 0;
        Debug.Log("Next goal selected: " + currentGoalPoint);
    }

    public void setEnemyState(EnemyStates n_es)
    {
        es = n_es;
    }
}

public enum EnemyStates { St_Patroling, St_Chasing };



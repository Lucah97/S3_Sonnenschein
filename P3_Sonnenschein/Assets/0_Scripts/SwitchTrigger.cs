using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    public bool startPosition = false;
    public bool repeatable = true;
    [Range(30,100)]
    public float dickAdjustDegree = 90f;
    public float flipSpeed = 1;
    public float distanceCutOff;
    public GameObject[] Targets;

    private Transform dickTransform;
    private float desiredDeg;
    private bool hasBeenActivated = false;

    private void Start()
    {
        dickTransform = transform.GetChild(0);
        dickTransform.Rotate(new Vector3(0, 0, (startPosition ? -1 : 1) * (dickAdjustDegree / 2)));
        desiredDeg = dickTransform.rotation.eulerAngles.z;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(dickTransform.rotation.eulerAngles.z - desiredDeg) > distanceCutOff)
        {
            dickTransform.rotation = Quaternion.Slerp(dickTransform.rotation,
                                                      Quaternion.Euler(new Vector3(0, 0, desiredDeg)),
                                                      flipSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Legs"))
        {
            if ((!repeatable && !hasBeenActivated) || (repeatable))
            {
                Destroy(other.gameObject);

                flipDick();
                hasBeenActivated = true;

                foreach (var target in Targets)
                {
                    target.GetComponent<InterfaceLetherTrigger>().OnSwitchTrigger();
                }
            }
        }
	}

    public void flipDick()
    {
        desiredDeg *= -1;
    }
}

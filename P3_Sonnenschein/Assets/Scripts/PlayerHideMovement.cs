using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerHideMovement : MonoBehaviour {

    public float returnPositionDelta = 0.1f;

    private HideZone hz;

    private bool isHidden = false;
    private bool exitHiding = false;
    private bool hasPressed = false;

    private Vector3 prevPosition = Vector3.zero;

    //### Built-In Functions ###
    private void LateUpdate()
    {
        if (!exitHiding)
        {
            //Process Hide Input
            if ((hideInput() && canHide()))
            {
                isHidden = !isHidden;
                setMovement(!isHidden);
                if (!isHidden)
                {
                    exitHiding = true;
                    setMovement(false);
                }
                hz.enableIndicatorLight(!isHidden);
                prevPosition = (isHidden) ? transform.position : prevPosition;
            }

            //Move To Position
            if (isHidden)
            {
                transform.position = Vector3.Lerp(transform.position, hz.getHidePosition(), Time.deltaTime * hz.hideAnimSpeed);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, prevPosition, Time.deltaTime * hz.hideAnimSpeed);
            if (Vector3.Distance(transform.position, prevPosition) < returnPositionDelta)
            {
                //transform.position = prevPosition;
                exitHiding = false;
                setMovement(true);
            }
        }
    }

    //### Custom Functions ###
    public void assignHideZone(HideZone n_hz)
    {
        hz = n_hz;
    }

    private void setMovement(bool setting)
    {
        GetComponent<PlayerMovement>().enabled = setting;
    }

    public bool canHide()
    {
        return (hz == null) ? false : true;
    }

    private bool hideInput()
    {
        if ((Input.GetAxis("Interact") == 1) && (!hasPressed))
        {
            hasPressed = true;
            return true;
        }

        if (Input.GetAxis("Interact") == 0)
        {
            hasPressed = false;
            return false;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour {

    public static CameraMovement instance;
    public Vector3 currentVelocity = Vector3.zero;

    private Vector3 desiredPosition = Vector3.zero;
    private Vector2 lastVelocityOffset = Vector2.zero;
    private GameObject player;
    private CameraMovementZone cmz;
    private BoxCollider cmzCollider;

    //### Built-In Functions ###
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        //Singleton Instance
        if ((instance != this) && (instance != null))
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
	
	void LateUpdate ()
    {
        if (cmz != null)
        {
            moveCamera();
        }
    }

    //### Custom Functions ###
    private void moveCamera()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 colCenter = cmzCollider.transform.TransformPoint(cmzCollider.center);

        //Setup desiredPosition
        desiredPosition.x = (cmz.followHorizontal ? playerPos.x : (colCenter.x));
        desiredPosition.y = (cmz.followVertical ? playerPos.y : (colCenter.y));
        desiredPosition.z = playerPos.z;

        //Add Player Velocity if needed
        if (cmz.followPlayerVelocity)
        {
            if (cmz.followHorizontal)
            {
                if (((Mathf.Abs(lastVelocityOffset.x) < (Mathf.Abs(player.GetComponent<Rigidbody>().velocity.x)))) || 
                    (oppositeSigns(lastVelocityOffset.x, player.GetComponent<Rigidbody>().velocity.x)))
                {
                    lastVelocityOffset.x = player.GetComponent<Rigidbody>().velocity.x;
                }
                desiredPosition.x += (lastVelocityOffset.x * cmz.playerVelocityMultiplier);
            }

            /*
            if (cmz.followVertical)
            {
                if ((Mathf.Abs(lastVelocityOffset.y) < (Mathf.Abs(player.GetComponent<Rigidbody>().velocity.y))) ||
                    (((int)player.GetComponent<Rigidbody>().velocity.y ^ (int)lastVelocityOffset.y) < 0))
                {
                    lastVelocityOffset.y = player.GetComponent<Rigidbody>().velocity.y;
                }
                desiredPosition.y += (lastVelocityOffset.y * cmz.playerVelocityMultiplier);
            }
            */
        }

        //Restrict position based on Collider
        Vector3 colPosition = getColPos(desiredPosition);
        colPosition.x += cmz.cameraOffset.x;
        colPosition.y += cmz.cameraOffset.y;
        colPosition.z = playerPos.z + cmz.cameraDistance;

        //SmoothDamp Camera Movement
        transform.position = Vector3.SmoothDamp(transform.position, colPosition, ref currentVelocity, cmz.cameraSmoothTime);
    }

    private Vector3 getColPos(Vector3 desPos)
    {
        Vector3 colCenter = cmzCollider.transform.TransformPoint(cmzCollider.center);
        Vector3 colSize = cmzCollider.size;

        Vector3 colPos = desPos;
        colPos.x = Mathf.Clamp(colPos.x, (colCenter.x - ((colSize.x / 2) * cmz.clipHorizontal.x)), (colCenter.x + ((colSize.x / 2) * cmz.clipHorizontal.y)));
        colPos.y = Mathf.Clamp(colPos.y, (colCenter.y - ((colSize.y / 2) * cmz.clipVertical.x)), (colCenter.y + ((colSize.y / 2) * cmz.clipVertical.y)));
        colPos.z = 0;

        return colPos;
    }

    private bool oppositeSigns(float x, float y)
    {
        return (x < 0) ? (y >= 0) : (y < 0);
    }

    public void assignCMZ(CameraMovementZone n_cmz)
    {
        if (cmz != null)
        {
            cmz.setLightsActive(false);
        }
        cmz = n_cmz;
        cmzCollider = cmz.gameObject.GetComponent<BoxCollider>();
        cmz.setLightsActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {

    public GameObject debugSphere;

    [Header("Movement Properties")]
    public bool allowInput = true;
    public float acceleration;
    public float maxMovSpeed;
    public float gravity;
    public float friction;
    public float jumpForce;
    public float zDepth;
    public float zDepthAdjustSpeed;
    public float rotationTime;

    private float lastZdepth;
    private bool canonMode = false;
    private bool applyGravity = true;

    [Header("Collision Properties")]
    [Range(60f, 90f)]
    public float slopeAngle = 45f;
    [Range(2,10)]
    public int collisionCheckDensity = 3;
    public float speedStepCheck = 0.25f;
    public float stepCheckOffset = 0.05f;
    public float stepHeight = 0.5f;
    public Vector2 collisionOffset;

    private float velX = 0;
    private bool hasJumped = false;
    private Rigidbody rb;

    //### Built-In Functions ###
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        zDepth = transform.position.z;

        //Setup Layer Collision
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 10, true);
    }

    private void FixedUpdate()
    {
        if (allowInput)
        {
            processInput();
            setAnimations();
        }

        //Check for wall collision
        if (hitWall(collisionCheckDensity))
        {
            velX = 0;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //Rotate with velocity in canon mode
        if (canonMode)
        {
            transform.up = (rb.velocity).normalized;
        }

        //Add Gravity
        if (applyGravity)
        {
            Vector3 nVel = rb.velocity;
            nVel -= new Vector3(0, Mathf.Abs(gravity), 0);
            rb.velocity = nVel;
        }

        //Z-Depth adjustment (Stairs)
        adjustZdepth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.CompareTag("Solid")) && (canonMode))
        {
            canonMode = false;
            transform.up = Vector3.up;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            velX = rb.velocity.x;
            setAllowIinput(true);

            CameraMovement.instance.setOverridePosition(false, Vector3.zero);
        }
    }

    //### Custom Functions ###
    private void processInput()
    {
        //Horizontal Movement 
        float horInput = Input.GetAxis("Horizontal");
        velX += (horInput * (acceleration * transform.localScale.x));
        velX *= friction;
        velX = Mathf.Clamp(velX, -(maxMovSpeed * transform.localScale.x), 
                                  (maxMovSpeed * transform.localScale.x));
        Vector3 curVel = rb.velocity;
        rb.velocity = new Vector3(velX, curVel.y, 0);

        //Jumping
        if ((Input.GetAxis("Jump") == 1) && (isGrounded(true)) && (!hasJumped))
        {
            rb.AddForce(new Vector3(0, (jumpForce * transform.localScale.y), 0));
            hasJumped = true;
        }
        if (Input.GetAxis("Jump") == 0)
        {
            hasJumped = false;
        }
    }

    private void setAnimations()
    {
        //Rotate Character
        Transform visChar = transform.GetChild(0);
        int dir = Mathf.Clamp((int)getPlayerDirection().x, -1, 1);
        Vector3 eulerChar = Vector3.zero;
        eulerChar.y = -dir * 90;

        //visChar.rotation = Quaternion.Euler(eulerChar);

        visChar.rotation = Quaternion.Slerp(visChar.rotation, Quaternion.Euler(eulerChar), Time.deltaTime * rotationTime);
    }

    private bool hitWall(int divAmount)
    {
        //Sanitize Input
        divAmount = Mathf.Clamp(divAmount, 2, 10);

        //Collision values (based on collider and scale)
        float colHeight = GetComponent<CapsuleCollider>().height;
        colHeight *= transform.localScale.y;

        float colRadius = GetComponent<CapsuleCollider>().radius;
        colRadius += collisionOffset.x;
        colRadius *= transform.localScale.x;

        Vector3 startPos = transform.position;
        startPos.y += (colHeight / 2);

        //Collision step amount
        float colStepAmount = (colHeight / (divAmount-1));
        colStepAmount -= (colStepAmount * collisionOffset.x);

        //Check colission
        bool hasHitWall = false;
        for (int i = 0; i < divAmount; i++)
        {
            Vector3 curPos = startPos;
            curPos.y -= (colStepAmount * i);

            RaycastHit hit;
            if (Physics.Raycast(curPos, -getPlayerDirection(), out hit, colRadius))
            {
                if (hit.collider.gameObject.CompareTag("Solid"))
                {
                    /*
                    float addStepHeight = 0;
                    if ((checkStepHeight(hit.point.y, ref addStepHeight)) && (!hasHitWall) && (!hasJumped) && (isGrounded(false)) && (angleTooSteep(hit.normal)))
                    {
                        Vector3 newPos = transform.position;
                        newPos.y += addStepHeight * 3;
                        transform.position = newPos;
                        
                        break;
                    }
                    */
                    if (angleTooSteep(hit.normal))
                    {
                        hasHitWall = true;

                        break;
                    }
                }
            }
        }
        return hasHitWall;
    }

    private bool angleTooSteep(Vector3 normal)
    {
        return (Vector3.Angle(normal, getPlayerDirection()) < slopeAngle);
    }

    private bool checkStepHeight(float curY, ref float addStep)
    {
        float colHeight = GetComponent<CapsuleCollider>().height;
        colHeight *= transform.localScale.y;

        float colRadius = GetComponent<CapsuleCollider>().radius;
        colRadius += (colRadius * (collisionOffset.x));
        colRadius *= transform.localScale.x;

        float playerFeet = transform.position.y - (colHeight / 2);

        //Calculate next highest free spot
        if ((curY - playerFeet) < getStepHeight())
        {
            float checkHeight = playerFeet;

            while (checkHeight < (playerFeet + getStepHeight()))
            {
                Vector3 curPos = transform.position;
                curPos.y = checkHeight;

                RaycastHit hit;
                if (!Physics.Raycast(curPos, -getPlayerDirection(), out hit, colRadius))
                {
                    //Step possibility found
                    Instantiate(debugSphere, hit.point, debugSphere.transform.rotation);
                    addStep = checkHeight - playerFeet;
                    return true;
                }
                else
                {
                    //Instantiate(debugSphere, hit.point, debugSphere.transform.rotation);
                }
                checkHeight += getStepCheckOffset();
            }
            //No step possibility found
            addStep = 0;
            return false;
        }
        else
        {
            //Can't step this high
            addStep = 0;
            return false;
        }
    }

    public bool isGrounded(bool mode)
    {
        RaycastHit hit;

        float colRadius = GetComponent<CapsuleCollider>().radius;
        colRadius += (colRadius * (collisionOffset.x));
        colRadius *= transform.localScale.x;

        float step = (colRadius * 2) / (collisionCheckDensity-1);

        bool hasFoundGround = true;

        for (int i=0; i<collisionCheckDensity; i++)
        {
            Vector3 checkPos = transform.position;
            checkPos.x -= colRadius;
            checkPos.x += (step * i);

            if (Physics.Raycast(checkPos, Vector3.down, out hit, getJumpDistance()))
            {
                if (hit.collider.gameObject.CompareTag("Solid"))
                { if (mode)  { return true; } }
                else { hasFoundGround = false; }
            }
            else
            {
                hasFoundGround = false;
            }
        }
        return hasFoundGround;
    }

    //### Setter / Getter ###
    public float getJumpDistance()
    {
        float colHeight = GetComponent<CapsuleCollider>().height;
        float dist = (colHeight / 2) * transform.localScale.y;
        dist += (colHeight * collisionOffset.y);

        return dist;
    }

    private void adjustZdepth()
    {
        if (transform.position.z != zDepth)
        {
            Vector3 desiredPos = transform.position;
            desiredPos.z = zDepth;

            transform.position = Vector3.Lerp(transform.position, desiredPos, zDepthAdjustSpeed * Time.deltaTime);
        }
    }

    private float getStepHeight()
    {
        return (stepHeight * transform.localScale.y);
    }

    private float getStepCheckOffset()
    {
        return (stepCheckOffset * transform.localScale.y);
    }

    public Vector3 getPlayerDirection()
    {
        Vector3 direction = Vector3.zero;
        direction.x = (rb.velocity.x < 0) ? 1 : -1;

        return direction;
    }

    public void setZdepth(float z, bool im)
    {
        if (!im)
        {
            lastZdepth = zDepth;
            zDepth = z;
        }
        if (im)
        {
            lastZdepth = zDepth;
            zDepth = z;

            Vector3 desiredPos = transform.position;
            desiredPos.z = zDepth;
            transform.position = desiredPos;
        }
    }

    public void setAllowIinput(bool i)
    {
        allowInput = i;
    }

    public void setCanonMode (bool c)
    {
        canonMode = c;
    }

    public void returnToLastZdepth()
    {
        zDepth = lastZdepth;
    }

    public void freezeVelocity()
    {
        velX = 0;
        rb.velocity = Vector3.zero;
    }

    public void setVelocity(Vector3 nVel)
    {
        rb.velocity = nVel;
    }

    public void setApplyGravity(bool g)
    {
        applyGravity = g;
    }
}

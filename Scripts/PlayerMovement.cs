using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 target;
    Vector3 lastPosition;
    Vector3 lookAt;
    bool reachedTarget = true;
    float speed = 2f;
    public Animator anim;
    bool isMoving = true;
    bool check = true;

    void Start()
    {
        walkingOff();
        target = transform.position;
        lookAt = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            isMoving = true;
            anim.SetBool("walking", true);
            target.z += 1;
            lookAt = transform.position;
            lookAt.z += 2;
            lastPosition = transform.position;
        }
        if (Input.GetKeyDown("s"))
        {
            isMoving = true;
            anim.SetBool("walking", true);
            target.z -= 1;
            lookAt = transform.position;
            lookAt.z -= 2;
            lastPosition = transform.position;
        }
        if (Input.GetKeyDown("d"))
        {
            isMoving = true;
            anim.SetBool("walking", true);
            target.x += 1;
            lookAt = transform.position;
            lookAt.x += 2;
            lastPosition = transform.position;
        }
        if (Input.GetKeyDown("a"))
        {
            isMoving = true;
            anim.SetBool("walking", true);
            target.x -= 1;
            lookAt = transform.position;
            lookAt.x -= 2;
            lastPosition = transform.position;
        }
        Debug.Log(isMoving);
        if (!isMoving)
        {
            lastPosition = transform.position;
        }
        else
        { 
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.LookAt(lookAt);
            if (transform.position == target)
            {
                anim.SetBool("walking", false);
                isMoving = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            target = lastPosition;
        }
    }

    void walkingOff()
    {
        anim.SetBool("walking", false);
    }

    void walkingOn()
    {
        anim.SetBool("walking", true);
    }
}

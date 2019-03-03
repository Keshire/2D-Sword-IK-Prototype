using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 m;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    
    //For inspector tweaking.
    public float runSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = m-transform.position;
        float angle = (Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg) + 90;
        //turn slightly slower from front
        if (Vector3.Dot(transform.up, target) < 0f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.AngleAxis(angle, Vector3.forward), turnSpeed * 0.5f);
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.AngleAxis(angle, Vector3.forward), turnSpeed);
        }
            


    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

    Rigidbody2D handPhysics;
    public Transform[] targets;
    public float speed = 1f;
    public float minDistance = 0.05f;

    Transform hilt;


    // Start is called before the first frame update
    void Start()
    {
        handPhysics = GetComponent<Rigidbody2D>();
        hilt = transform.Find("Hilt");
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = GetClosestTarget(targets);
        Vector3 hand = transform.transform.position;
        Vector3 targetDirection = (target.position - hand).normalized;

        if (Vector3.Distance(target.position, hand) > minDistance)
        {
            transform.position = Vector2.MoveTowards(hand, targetDirection, speed * Time.deltaTime);
            //hilt.localEulerAngles = target.localEulerAngles;
            hilt.rotation = target.rotation;
        }
    }

    private void FixedUpdate()
    {
        //handPhysics.MovePosition(hand + targetDirection * speed * Time.fixedDeltaTime);

    }

    Transform GetClosestTarget(Transform[] targets)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - m;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

    Rigidbody2D handPhysics;
    public Transform[] targets;
    //public float speed = 0.001f;
    public float minDistance = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        handPhysics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = GetClosestTarget(targets).position;
        Vector3 hand = transform.transform.position;
        Vector3 targetDirection = (target - hand).normalized;

        if (Vector3.Distance(target, hand) > minDistance)
            transform.position = Vector2.MoveTowards(hand, targetDirection, 0.01f);
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

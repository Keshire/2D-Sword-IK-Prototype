using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    Rigidbody2D hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hand.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}

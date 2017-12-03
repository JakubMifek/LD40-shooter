using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Deletable : MonoBehaviour
{
    private Rigidbody rb;
    public ScoreCounter sc;
    public int score;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < 0)
        {
            if(sc != null)
                sc.Score += score;

            Destroy(gameObject);
        }
    }
}

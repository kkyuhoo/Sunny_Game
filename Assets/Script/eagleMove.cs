using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleMove : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    Animator anim;
    SpriteRenderer spriteRenderer;


    private float maxSpeed = 2f;
    private Vector3 oriPos;
    private float distance = 1f;
    private Vector2 dir = Vector3.up;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        oriPos = transform.position;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        var pos = enemyRigidbody.position;
        pos += dir * maxSpeed * Time.deltaTime;
        enemyRigidbody.MovePosition(pos);

        if (oriPos.y + distance < enemyRigidbody.position.y)
        {
            dir = Vector3.down;
        }
        if (oriPos.y - distance > enemyRigidbody.position.y)
        {
            dir = Vector3.up;
        }

    }

}

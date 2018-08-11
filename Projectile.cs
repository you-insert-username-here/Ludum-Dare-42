using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour 
{
    public float timeToLive;
    public float timeAlive;

    public int damage;
    public int moveSpeed;

    private Rigidbody2D rb2d;
    private Collider2D col2d;

    private void Start()
    {
        moveSpeed = 2;
        timeToLive = 10;
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();        
    }

    private void Update()
    {
        rb2d.velocity = transform.right * moveSpeed;
        timeAlive += 1.0f * Time.deltaTime;

        if (timeAlive > timeToLive)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroyed: HIT");
        Destroy(this.gameObject);
    }
}
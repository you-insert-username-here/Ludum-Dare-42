using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Projectile : MonoBehaviour 
{
    public float timeToLive;
    public float timeAlive;

    private int damage;
    public int moveSpeed;

    private Rigidbody2D rb2d;
    private Collider2D col2d;
    GameObject playerCharacter;

    private void Start()
    {
        playerCharacter = GameObject.Find("Player");
        moveSpeed = 12;
        timeToLive = 10;
        damage = playerCharacter.GetComponent<PlayerCharacter>().attackDamage;

        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        rb2d.velocity = transform.up * moveSpeed;
        timeAlive += 1.0f * Time.deltaTime;

        if (timeAlive > timeToLive)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);

        if (collision.gameObject.tag == "Player")
            collision.GetComponent<PlayerCharacter>().health -= damage;
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyCharacters>().health -= damage;
            collision.GetComponent<EnemyCharacters>().PlaySound(collision.GetComponent<EnemyCharacters>().hit);
        }
    }
}
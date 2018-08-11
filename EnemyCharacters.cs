using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCharacters : MonoBehaviour 
{
    public float moveSpeed;
    public int health;

    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public float swingTimer, swingCounter;


    public GameObject projectilePrefab;
    public Transform attackLocation;
    public Transform attackTarget;
    public GameObject deadEnemy;

    private Collider2D col2d;
    private Rigidbody2D rb2d;
    private Vector2 currentPosition;
    

    private void Start()
    {
        transform.gameObject.tag = "Enemy";

        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        moveSpeed = 1.2f; //Multiply by 5 to achieve same as playercharacter.
        attackSpeed = 5;
        health = 5;
        attackRange = 2;
        swingTimer = 5.0f;
    }

    private void Update()
    {
        currentPosition = this.transform.position;
        Death();
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Attack()
    {
        if(Vector3.Distance(attackTarget.position, transform.position) < attackRange && attackTarget.gameObject.tag == "Player" && swingTimer == swingCounter)
        {
            swingCounter = 0.0f;
            attackTarget.GetComponent<PlayerCharacter>().health -= 1;
        }
        if (swingCounter < swingTimer)
            swingCounter += 1.0f * Time.deltaTime;
        else if(swingCounter > swingTimer)
            swingCounter = swingTimer;

    }

    void Movement()
    {
        float angle = Mathf.Atan2(attackTarget.position.y, attackTarget.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //rb2d.AddForce(new Vector2((transform.position.x - attackTarget.position.x) * -moveSpeed, (transform.position.y - attackTarget.position.y) * -moveSpeed));
        float step = (moveSpeed * 5.0f) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, attackTarget.position, step);
    }

    void TakeDamage()
    {
    }

    void Death()
    {
        if (health <= 0)
        {
            Instantiate(deadEnemy, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}

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
    public GameObject playerCharacter;

    private Collider2D col2d;
    private Rigidbody2D rb2d;
    private Vector2 currentPosition;
    private Animator animator;

    private AudioSource audioSource;
    public AudioClip attack, hit, die;


    private void Start()
    {
        transform.gameObject.tag = "Enemy";

        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        moveSpeed = 0.5f; //Multiply by 5 to achieve same as playercharacter.
        attackSpeed = 5;
        health = 5;
        attackRange = 2.5f;
        swingTimer = 5.0f;

        playerCharacter = GameObject.Find("Player");
        attackTarget = playerCharacter.transform;
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
            animator.SetBool("isRunning", false);
            animator.SetTrigger("isAttacking");
            swingCounter = 0.0f;
            attackTarget.GetComponent<PlayerCharacter>().health -= 1;
            attackTarget.GetComponent<PlayerCharacter>().PlaySound(attackTarget.GetComponent<PlayerCharacter>().hit);
        }
        if (swingCounter < swingTimer)
            swingCounter += 1.0f * Time.deltaTime;
        else if(swingCounter > swingTimer)
            swingCounter = swingTimer;

    }

    void Movement()
    {
        Vector3 vectorToTarget = attackTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        animator.SetTrigger("isRunning");
        /*
        //float angle = Mathf.Atan2(attackTarget.position.y, attackTarget.position.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(attackTarget.position.y, attackTarget.position.x) * Mathf.Rad2Deg;

        if(Input.GetButtonDown("Jump"))
            Debug.Log(angle);



        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        */
        //rb2d.AddForce(new Vector2((transform.position.x - attackTarget.position.x) * -moveSpeed, (transform.position.y - attackTarget.position.y) * -moveSpeed));
        float step = (moveSpeed * 5.0f) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, attackTarget.position, step);        
    }

    void Death()
    {
        if (health <= 0)
        {
            //animator.SetTrigger("isDead");
            Instantiate(deadEnemy, transform.position, transform.rotation);
            attackTarget.GetComponent<PlayerCharacter>().IncrementScore();
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio, 0.6f);
    }
}
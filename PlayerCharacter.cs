using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour
{
    #region Public Variables

    public float moveSpeed;
    public int score;

    //Health Tracker
    public int health;
    public int maxHealth;

    public int attackDamage;
    public int attackSpeed;

    public GameObject projectilePrefab;
    public GameObject deadPlayer;
    public Transform attackLocation;
    public float attackTimer, attackCounter;
    public float deathTimer;

    //Quaternion facingDirection;
    private Animator animator;

    #endregion

    #region Private Variables

    private Collider2D col2d;
    private Rigidbody2D rb2d;
    private Vector2 currentPosition;
    private float horizontalMovement, verticalMovement;

    private AudioSource audioSource;
    public AudioClip attack, hit, die;
    public GameObject canvasObject;
    public Text endingText;
    public bool gameWon;

    #endregion

    private void Start()
    {
        transform.gameObject.tag = "Player";
        this.name = "Player";

        canvasObject = GameObject.Find("CanvasPrefab");

        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        moveSpeed = 1.2f;
        attackSpeed = 5;    
        attackTimer = 0.5f;
        score = 0;
        gameWon = false;
        deathTimer = 0;
    }

    private void Update()
    {
        currentPosition = this.transform.position;

        endingText = Text.FindObjectOfType<Text>();
        endingText.text = "Congratulations!\nYou Killed " + score + " Orcs!";

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        MouseLook2D();

        if (health <= 0)
            Death();


        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }

        Attack();
        GameEnd();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
        movement.Normalize();

        rb2d.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
    }

    void MouseLook2D()
    {
        /// Mouse Look
        /// Created function to assist with 2D topdown mouse follow
        /// Put into Update() to have the 2D sprite follow the mouse.

        #region MouseLook2D() Code
        if (deathTimer == 0)
        {
            //Store Mouse Position
            Vector3 mousePosition = Input.mousePosition;
            //Get this objects position 
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            //Calculate difference between mouse and object
            mousePosition.x = mousePosition.x - objectPosition.x;
            mousePosition.y = mousePosition.y - objectPosition.y;
            //Get a usable angle using MathF nad transform radians to degrees
            //float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            //Change the objects rotation
            float angle = Mathf.Atan2(-mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //facingDirection = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        #endregion
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && deathTimer == 0)
        {
            if (attackCounter == attackTimer)
            {
                PlaySound(attack);
                animator.SetTrigger("isAttacking");

                attackCounter = 0.0f;
                GameObject fireball = Instantiate(projectilePrefab, attackLocation.position, attackLocation.rotation) as GameObject;

            }
        }

        if (attackCounter < attackTimer)
        {
            attackCounter += 1.0f * Time.deltaTime;
        }
        else if (attackCounter > attackTimer)
            attackCounter = attackTimer;
    }

    void GameEnd()
    {
        if (score >= 42)
        {
            gameWon = true;
        }
    }

    public void IncrementScore()
    {
        Debug.Log("Incrementing Score");
        score += 1;
        Debug.Log(score);
    }

    void Death()
    {
        PlaySound(die);
        animator.SetBool("isDead", true);
        deathTimer += 1.0f * Time.deltaTime;
        if (deathTimer >= 4.0f)
            Time.timeScale = 0;

        //SceneManager.LoadScene("Title");
        //Instantiate(deadPlayer, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio, 0.8f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour
{
    #region Public Variables

    public float moveSpeed;
    public int health;

    public int attackDamage;
    public int attackSpeed;

    public GameObject projectilePrefab;
    public Transform attackLocation;
    private List<GameObject> projectiles = new List<GameObject>();

    Quaternion facingDirection;

    #endregion

    #region Private Variables

    private Collider2D col2d;
    private Rigidbody2D rb2d;
    private Vector2 currentPosition;
    private float horizontalMovement, verticalMovement;

    #endregion

    private void Start()
    {
        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        moveSpeed = 1.2f;
        attackSpeed = 5;
    }

    private void Update()
    {
        currentPosition = this.transform.position;

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        MouseLook2D();

        if (Input.GetButtonDown("Fire1"))
            Attack();
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
        //Store Mouse Position
        Vector3 mousePosition = Input.mousePosition;
        //Get this objects position 
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        //Calculate difference between mouse and object
        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;
        //Get a usable angle using MathF nad transform radians to degrees
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        //Change the objects rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        facingDirection = Quaternion.Euler(new Vector3(0, 0, angle));
        #endregion
    }

    void Attack()
    {
        Debug.Log("Fire");
        GameObject fireball = Instantiate(projectilePrefab, attackLocation.position, attackLocation.rotation) as GameObject;
    }

    void TakeDamage()
    {

    }

    void Death()
    {

    }
}

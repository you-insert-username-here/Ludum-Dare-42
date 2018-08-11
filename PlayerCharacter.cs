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
    }

    private void Update()
    {
        currentPosition = this.transform.position;

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
        movement.Normalize();
        
        rb2d.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
    }
}

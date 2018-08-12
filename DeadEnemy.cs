using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadEnemy : MonoBehaviour 
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip die;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(die, 0.8f);
    }

    private void Start()
    {
        animator.SetTrigger("isDead");
    }
}
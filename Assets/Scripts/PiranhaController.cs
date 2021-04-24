using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaController : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private float attackInterval = 1.5f;
    private float attackIntervalTimer;

    private int health;
    public int maxHealth = 5;
    public AudioClip audioHit;
    public AudioClip audioAttack;
    public HealthBarController hbct;

    private void Start() {
        animator = GetComponent<Animator>();
        health = maxHealth;
        attackIntervalTimer = attackInterval;
        hbct.setMaxHP(maxHealth);
    }

    private void Update() {
        if (isAttacking && (attackIntervalTimer -= Time.deltaTime) < 0)
            { isAttacking = false; attackIntervalTimer = attackInterval; }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(isAttacking) return;

        PlayerController ct = other.GetComponent<PlayerController>();
        if (ct != null)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            AudioSource.PlayClipAtPoint(audioAttack, transform.position, 1f);
        }
    }

    public void hit() {
        animator.SetTrigger("Hit");
        health--;
        AudioSource.PlayClipAtPoint(audioHit, transform.position, 1f);
        Debug.Log("was hit and now health :"+health);
        hbct.setHP(health);
        if (health <= 0) { Invoke("die", 0.4f); }
    }

    private void die() { Destroy(gameObject); }

    private void OnCollisionEnter2D(Collision2D other) {
        PlayerController ct = other.gameObject.GetComponent<PlayerController>();
        if(ct!=null) ct.changeHP(-1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugController : MonoBehaviour
{
    private bool isAttacking = false;
    private float attackInterval = 1.5f;
    private float attackIntervalTimer;

    public AudioClip audioAttack;

    private void Start() {
        attackIntervalTimer = attackInterval;
    }

    private void Update() {
        if (isAttacking && (attackIntervalTimer -= Time.deltaTime) < 0)
            { isAttacking = false; attackIntervalTimer = attackInterval; }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(isAttacking) return;
        PlayerController ct = other.gameObject.GetComponent<PlayerController>();
        if (ct != null)
        {
            isAttacking = true;
            AudioSource.PlayClipAtPoint(audioAttack, transform.position, 1f);
            ct.changeHP(-1);
        }
    }
}

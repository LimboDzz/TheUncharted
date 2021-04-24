using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;                               // Components
    private Animator animator;
    private Collider2D cd2d;
    private Collider2D cd2dForGround;
    private AudioSource audioSource;
    private float x,y;                                      // Input.GetAxis
    private float heading = 1.0f;                           // 保存朝向
    private bool setToJump = false;
    private bool isOnGround = true;
    private float launchPosY = 1.3f;                        // Yposition of projectile
    private float launchIntervalTimer;
    private bool isLaunching = false;
    private float launchInterval = 0.7f;

    private int health;                                     // PlayerAttributes
    public int maxHealth = 5;
    public int hp {get { return health; } }
    public float speed = 3.5f;
    public float jumpForce = 50.0f;
    public GameObject projectilePrefab;
    public float launchSpeed = 700;
    public float launchLaterncy = 0.1f;
    public AudioClip audioLaunch;
    public AudioClip audioInjured;
    public HealthBarController hbct;
    public GameManager gameManager;

    private void Start() {
        rb2d=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        cd2d = GetComponent<Collider2D>();
        cd2dForGround = GameObject.FindWithTag("Env").GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        launchIntervalTimer = launchInterval;
        health = maxHealth-2;
        hbct.setMaxHP(maxHealth);
        hbct.setHP(health);
    }

    public void changeHP(int amount)
    {
        if (amount < 0) { 
            animator.SetTrigger("Hit"); 
            AudioSource.PlayClipAtPoint(audioInjured, transform.position, 0.25f);
            if(health<=0) Invoke("gameover", 1f);
        }
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        hbct.setHP(health);
        // Debug.Log("Remaining HP: "+health);
    }
    private void gameover(){
        gameManager.gameOver();
    }

    private void Update() {
        setAnimatorBasics();
        if (Input.GetKeyDown(KeyCode.L)) launch();
        if (!isOnGround && cd2d.IsTouching(cd2dForGround)) 
            { isOnGround = true; }
        if (Input.GetKeyDown(KeyCode.W)) setToJump = true;
        if (isLaunching && (launchIntervalTimer-=Time.deltaTime) < 0) 
            { isLaunching = false; launchIntervalTimer = launchInterval; };
        if (Input.GetKeyDown(KeyCode.Escape)) Invoke("gameover", 0.5f);
    }

    private void FixedUpdate(){
        move();
        jump();
    }

    private void setAnimatorBasics(){
        x=Input.GetAxis("Horizontal");
        y=Input.GetAxis("Vertical");
        setHeading();
        animator.SetFloat("Heading",heading);
        animator.SetFloat("Speed",Mathf.Abs(x));
    }

    private void setHeading(){
        if(!Mathf.Approximately(x,0)){
            heading=x>0?1:-1;
        }
    }

    private void move(){
        // Vector2 pos=rb2d.position;
        // pos+=new Vector2(x,0)*speed*Time.deltaTime;
        // rb2d.MovePosition(pos);
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
    }

    private void launch(){
        if(isLaunching||!isOnGround) return;
        isLaunching = true;
        animator.SetTrigger("Launch");
        AudioSource.PlayClipAtPoint(audioLaunch, transform.position, 0.07f);
        Invoke("laucnhProjectile", launchLaterncy);
    }

    private void laucnhProjectile(){
        GameObject projectile = Instantiate(projectilePrefab, rb2d.position + Vector2.up * launchPosY, Quaternion.identity);
        ProjectileController ct = projectile.GetComponent<ProjectileController>();
        ct.launch(new Vector2(heading,0), launchSpeed);
    }

    private void jump(){
        if(setToJump&&isOnGround){
            setToJump = false;
            isOnGround = false;
            animator.SetTrigger("Jump");
            rb2d.velocity=new Vector2(rb2d.velocity.x,jumpForce);
        }
    }
}

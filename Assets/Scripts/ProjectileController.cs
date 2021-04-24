using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float lifetime = 0.5f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    public void launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if((lifetime-=Time.deltaTime)<0) Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        PiranhaController ct = other.collider.GetComponent<PiranhaController>();
        if (ct != null) ct.hit();
        Destroy(gameObject);
    }
}

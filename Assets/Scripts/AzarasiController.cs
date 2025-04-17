using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AzarasiController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    public bool IsDead{
        get{return isDead;}
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && transform.position.y < maxHeight){
            Flap();
        }

        ApplyAngle();

        animator.SetBool("flap",angle >= 0.0f && !isDead);
    }

    public void Flap(){
        if(isDead) return;
        
        if(rb2d.isKinematic)return;

        rb2d.velocity = new Vector2(0.0f,flapVelocity);
    }

    void ApplyAngle(){
        float targetAngele;
        if(isDead){
            targetAngele = 180.0f;  
        }else{
            targetAngele=Mathf.Atan2(rb2d.velocity.y,relativeVelocityX) * Mathf.Rad2Deg;
        }

        angle = Mathf.Lerp(angle, targetAngele, Time.deltaTime * 10.0f);

        sprite.transform.localRotation = Quaternion.Euler(0.0f,0.0f,angle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDead)return;
        isDead = true;     
    }

    public void SetSteerActive(bool active){
        rb2d.isKinematic = !active;
    }
}

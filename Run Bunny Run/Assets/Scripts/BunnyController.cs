﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    public Text scoretext;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
        }
        if(bunnyHurtTime == -1)
        {
            if((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)
            {
                if(myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }
                if(jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
                } else
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce);
                }
                
                jumpsLeft--;
                jumpSfx.Play();
            }
            myAnimator.SetFloat("Velocity", myRigidBody.velocity.y);
            scoretext.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if(Time.time > bunnyHurtTime + 2)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }
            foreach (MoveLeft modelefter in FindObjectsOfType<MoveLeft>())
            {
                modelefter.enabled = false;
            }
            bunnyHurtTime = Time.time;
            myAnimator.SetBool("BunnyHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
            myCollider.enabled = false;
            deathSfx.Play();
        } 
        else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            jumpsLeft = 2;
        }
    }
}

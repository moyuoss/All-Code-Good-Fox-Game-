using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContro : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D Coll;

    [SerializeField] private Text cherryText;

    [SerializeField] private float hurtForce = 5f;

    [SerializeField] private LayerMask ground;

    public int cherryNum = 0;

    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;
    
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        
        AnimationState();
        anim.SetInteger("state", (int)state);
    }
    void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            
        }

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            
        }
       


        if (Input.GetButtonDown("Jump") && Coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        state = State.jumping;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cherry")
        {
            Destroy(collision.gameObject);
            cherryNum++;
            cherryText.text = cherryNum.ToString();
        }
    }

    void AnimationState() 
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (Coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                {
                    state = State.idle;
                }
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = State.running;
        }
        else
            state = State.idle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "frog")
        {
            Frog frog = collision.gameObject.GetComponent<Frog>();

            if (state == State.falling)
            {
                frog.JumpON();
                Jump();
            }
            else 
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                } else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
            
        }else if (collision.gameObject.tag == "Eagle")
        {
            Eagle eagle = collision.gameObject.GetComponent<Eagle>();

            if (state == State.falling)
            {
                eagle.JumpON();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }


    }
}

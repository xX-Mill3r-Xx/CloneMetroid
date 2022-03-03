using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;
    public float radius;
    public float speed;
    public float forceJump;
    private bool isJumping = false;
    private bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Atack();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);
        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);

            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(movement == 0 && !isJumping)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
                isJumping = false;
            }
        }
    }

    void Atack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius);
            if (hit != null)
            {
                Debug.Log(hit.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
}

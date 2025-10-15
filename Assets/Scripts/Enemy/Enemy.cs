using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private GameObject destroyEffect;
   private Vector3 direction;
   [SerializeField] private float moveSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {   //face the player
        if (PlayerController.Instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX=true;
        }
        else
        {
            spriteRenderer.flipX=false;
        }
        //move towards the player
        direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x*moveSpeed,direction.y*moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(destroyEffect,transform.position, transform.rotation);
        }
    }
}

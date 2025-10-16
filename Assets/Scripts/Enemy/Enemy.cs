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
    {
        if (PlayerController.Instance.gameObject.activeSelf == true)
        {
            //face the player
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
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(1);
            Destroy(gameObject);
            Instantiate(destroyEffect,transform.position, transform.rotation);
        }
    }
}

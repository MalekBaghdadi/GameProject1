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
   [SerializeField] private float damage;
   [SerializeField] private float health;
   [SerializeField] private int experienceToGive;
   [SerializeField] private float pushTime;
   private float pushCounter;
   
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
                    //push enemy
                    if (pushCounter > 0)
                    {
                        pushCounter -= Time.deltaTime;
                        if (moveSpeed > 0)
                        {
                            moveSpeed = -moveSpeed;
                        }

                        if (pushCounter <= 0)
                        {
                            moveSpeed = Mathf.Abs(moveSpeed);
                        }
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
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    public void takeDamage(float damage)
    {
        health -=  damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        pushCounter = pushTime;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect,transform.position, transform.rotation);
            PlayerController.Instance.GetExperience(experienceToGive);
            AudioController.Instance.PlayModifiedSound(AudioController.Instance.enemyDeath);
        }
    }
}

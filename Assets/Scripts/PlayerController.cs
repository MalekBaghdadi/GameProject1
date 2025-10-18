using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController Instance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public Vector3 playerMoveDirection;
    [SerializeField] Animator animator;
    public float playerHealth;
    public float playerMaxHealth;
    public int experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> playerLevels;
    private bool isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;
    public Weapon activeWeapon;
    [SerializeField] private GameObject deathParticlesPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this; 
        }
        
    }

    void Start()
    {
        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }
        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateExperienceSlider();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX =Input.GetAxisRaw("Horizontal");
        float inputY =Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputX,inputY).normalized;
        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);
        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }

        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            isImmune = false;
        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunityDuration;
            playerHealth -= damage;
                    UIController.Instance.UpdateHealthSlider();
                    if (playerHealth <= 0)
                    {
                        if (deathParticlesPrefab != null)
                        {
                            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
                        }
                        gameObject.SetActive(false);
                        GameManager.Instance.GameOver();
                    }
        }
    }

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;
        UIController.Instance.UpdateExperienceSlider();
        if (experience >= playerLevels[currentLevel - 1])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel - 1];
        currentLevel++;
        UIController.Instance.UpdateExperienceSlider();
        UIController.Instance.levelUpButtons[0].ActivateButton(activeWeapon);
        UIController.Instance.LevelUpPanelOpen();
    }
}

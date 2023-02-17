using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Components 
    private Rigidbody2D rb;
    private AudioManager audio;
    private GameObject gameManager;
    #endregion
    #region Movement Variables 
    float normalMovementSpeed = 5f;
    float movementSpeed;
    float speedBoostDuration = 5f;
    Vector2 playerOrientation = Vector2.right;
    Vector2 currentDirection;
    #endregion
    #region Attack Variables
    float attackCooldown = 0.5f;
    float hitBoxTiming = 0.15f;
    float attackRange = 0.25f;
    bool canAttack = true;
    [SerializeField]
    private float currentBloodLevel;
    private float maxBloodLevel = 10;
    private float bloodLevelDecreaseAmount = 1;
    private float bloodDropRate = 5f;
    #endregion
    #region Animation Components
    Animator animationController;
    #endregion
    #region Health Variables
    public float maxHealth = 3;
    float currentHealth;
    float playerAttackDamage = 1;
    #endregion
    #region UI/UX
    public Slider HPSlider;
    public Slider BloodSlider;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
        gameManager = GameObject.FindWithTag("GameController");
        currentHealth = maxHealth;
        currentBloodLevel = maxBloodLevel + 1;
        movementSpeed = normalMovementSpeed;
        UpdateHPSlider();
        UpdateBloodSlider();
        StartCoroutine(DecreaseBloodLevel());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            Attack();
            canAttack = false;
        } 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
        Move();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        animationController.SetBool("isMoving", true);

        if (inputX < 0)
        {
            currentDirection = Vector2.left;
        }
        else if (inputX > 0)
        {
            currentDirection = Vector2.right;
        }
        else if (inputY < 0)
        {
            currentDirection = Vector2.down;
        }
        else if (inputY > 0)
        {
            currentDirection = Vector2.up;
        }
        else
        {
            currentDirection = Vector2.zero;
            animationController.SetBool("isMoving", false);
        }
        
        if (currentDirection != Vector2.zero) 
        {
            playerOrientation = currentDirection;
            animationController.SetFloat("DirX", currentDirection.x);
            animationController.SetFloat("DirY", currentDirection.y);
        }
        rb.velocity = currentDirection * movementSpeed;
    }

    #region Attack 
    void Attack()
    {
        animationController.SetTrigger("isAttacking");
        Debug.Log("ATttACK");
        StartCoroutine(AttackCooldownRoutine());
    }

    IEnumerator AttackCooldownRoutine()
    {
        Debug.Log(Vector2.one * attackRange);
        yield return new WaitForSeconds(hitBoxTiming);
        RaycastHit2D[] enemiesHit = Physics2D.BoxCastAll(rb.position + playerOrientation, Vector2.one * attackRange, 0f, Vector2.zero);
        //Debug.DrawRay();
        foreach (RaycastHit2D enemy in enemiesHit)
        {
            Debug.Log(enemy.transform.name);
            if (enemy.transform.tag == "Enemy")
            {
                Debug.Log("JILED");
                enemy.transform.GetComponent<Enemy>().TakeDamage(playerAttackDamage);
            }
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    #endregion

    #region Health System
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHPSlider();
        audio.Play("PlayerHurt");
        Debug.Log("Health is now " + currentHealth);
        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    void TriggerDeath()
    {
        audio.Play("PlayerDeath");
        gameManager.GetComponent<GameManager>().LoseGame();
        Destroy(this.gameObject, 0.5f);
    }

    public void Heal(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHPSlider();
    }
    #endregion

    void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + playerOrientation, new Vector2(0.5f, 0.5f), 0.5f, Vector2.zero, 0f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Chest")
            {
                hit.transform.GetComponent<Chest>().Interact();
            }
        }
    }

    #region UI Functions
    void UpdateHPSlider()
    {
        HPSlider.value = currentHealth / maxHealth;
    }
    void UpdateBloodSlider()
    {
        BloodSlider.value = currentBloodLevel / maxBloodLevel;
        Debug.Log(currentBloodLevel + " / " + maxBloodLevel + ": " + currentBloodLevel / maxBloodLevel);
    }
    #endregion


    #region Blood System
    IEnumerator DecreaseBloodLevel()
    {
        while (true)
        {
            currentBloodLevel -= bloodLevelDecreaseAmount;
            UpdateBloodSlider();
            if (currentBloodLevel <= 0)
            {
                TriggerDeath();
            }
            yield return new WaitForSeconds(bloodDropRate);
        }
    }

    public void FeedPlayer(float refillAmount)
    {
        currentBloodLevel += refillAmount;
        UpdateBloodSlider();
    }
    #endregion

    #region Movement Functions
    public void IncreaseMovementSpeed(float increaseMultiplier)
    {
        movementSpeed *= increaseMultiplier;
        StartCoroutine(MovementSpeedIncreaseRoutine());
    }

    IEnumerator MovementSpeedIncreaseRoutine()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        movementSpeed = normalMovementSpeed;
    }
    #endregion
}

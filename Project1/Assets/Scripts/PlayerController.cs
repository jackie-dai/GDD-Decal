using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Components 
    private Rigidbody2D rb;
    private AudioManager audio;
    #endregion
    #region Movement Variables 
    float movementSpeed = 5f;
    Vector2 playerOrientation = Vector2.right;
    Vector2 currentDirection;
    #endregion
    #region Attack Variables
    float attackCooldown = 0.5f;
    float hitBoxTiming = 0.25f;
    float attackRange = 0.25f;
    bool canAttack = true;
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
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
        currentHealth = maxHealth;
        UpdateHPSlider();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            Attack();
            canAttack = false;
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
        Destroy(this.gameObject);
    }

    public void Heal(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHPSlider();
    }
    #endregion

    void UpdateHPSlider()
    {
        HPSlider.value = currentHealth / maxHealth;
    }
    /*
    public static void DrawBoxCast2D(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, Color color)
    {
        Quaternion angle_z = Quaternion.Euler(0f, 0f, angle);
        DrawBoxCast2D(origin, size / 2f, direction, angle_z, distance, color);
    }
    */
}

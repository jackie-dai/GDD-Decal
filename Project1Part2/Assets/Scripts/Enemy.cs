using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Prefabs
    public Transform explosionPrefab;
    AudioManager audio;
    [SerializeField]
    private Transform blood;
    private int minBloodDrop = 3;
    private int maxBloodDrop = 6;
    #endregion
    #region Movement Variables
    public float movementSpeed = 2f;
    #endregion

    #region Health Variables
    public float maxHealth = 1f;
    float currentHealth;
    #endregion

    float explosionRadius = 5f;
    float explosionDamage = 1f;

    #region Physics Components
    Rigidbody2D rb;
    public Transform player;
    #endregion

    #region Unity Functions
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = FindObjectOfType<AudioManager>();
        currentHealth = 1;
    }
    #endregion

    #region Movement Functions
    void Update()
    {
        if (player)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        rb.velocity = directionToPlayer.normalized * movementSpeed;
    }
    #endregion

    #region Attack Functions
    void Explode()
    {
        audio.Play("Explosion");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("BOOM!");
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);
            }
        }
        Destroy(this.gameObject);
    }
    #endregion

    #region Damage System 
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        audio.Play("BatHurt");
        Debug.Log("Enemy die " + currentHealth);
        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    void SpawnBlood()
    {
        for (int i = 0; i < Random.Range(minBloodDrop, maxBloodDrop); i++)
        {
            Instantiate(blood, transform.position, transform.rotation);
        }
    }
    void TriggerDeath()
    {
        audio.Play("");
        SpawnBlood();
        Destroy(this.gameObject);
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Explode();
        }
    }
}

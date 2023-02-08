using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    #region Healing Variables
    [SerializeField]
    [Tooltip("Amount player heals")]
    float healAmount = 1f;
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Heal(healAmount);
            FindObjectOfType<AudioManager>().Play("Potion");
            Destroy(this.gameObject);
        }
    }

}

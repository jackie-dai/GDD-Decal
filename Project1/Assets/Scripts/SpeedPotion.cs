using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    private float speedBoostAmount = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.IncreaseMovementSpeed(speedBoostAmount);
            Destroy(this.gameObject);
        }
    }
}

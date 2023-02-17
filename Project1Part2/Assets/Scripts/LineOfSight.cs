using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (transform.parent.tag == "Enemy")
            {
                GetComponentInParent<Enemy>().player = other.transform;
            }
            
            if (transform.parent.tag == "Chest")
            {
                transform.parent.GetComponent<Chest>().ToggleOnText();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.parent.tag == "Chest")
        {

            transform.parent.GetComponent<Chest>().ToggleOffText();
        }
    }
}

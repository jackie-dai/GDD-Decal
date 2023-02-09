using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObjects
    public Transform healthPackPrefab;
    public GameObject text;
    #endregion 

    public void ToggleOnText()
    {
        text.SetActive(true);
    }
    public void ToggleOffText()
    {
        text.SetActive(false);
    }

    public void Interact()
    {
        Instantiate(healthPackPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject, 0.5f);
    }
}

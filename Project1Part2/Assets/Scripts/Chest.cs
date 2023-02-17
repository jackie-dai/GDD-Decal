using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObjects
    [SerializeField]
    private Transform[] items;
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
        int randomItemIndex = Random.Range(0, items.Length);
        Instantiate(items[randomItemIndex], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}

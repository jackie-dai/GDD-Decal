using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObjects
    public Transform healthPackPrefab;
    #endregion 

    public void Interact()
    {
        Instantiate(healthPackPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject, 0.5f);
    }
}

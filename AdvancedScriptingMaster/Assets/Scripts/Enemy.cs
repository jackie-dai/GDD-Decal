using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float horizontalBound = 10f;
    float verticalBound = 6f;
    Vector2 startingPosition;
    float radius = 5f;
    float elapsedTime = 0;

    void Start()
    {
        startingPosition = new Vector2(Random.Range(-horizontalBound, horizontalBound), Random.Range(-verticalBound, verticalBound));
        transform.position = startingPosition;
    }

    void Update()
    {
        transform.position = startingPosition + new Vector2(radius * Mathf.Sin(elapsedTime), radius * Mathf.Sin(elapsedTime));
        elapsedTime += Time.deltaTime;
    }
}

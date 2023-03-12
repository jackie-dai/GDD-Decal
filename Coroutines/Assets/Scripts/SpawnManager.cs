using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject sentry;
    public Transform[] spawnPositions;
    [SerializeField]
    private float spawnRate = 2f;
	// Use this for initialization
	void Start () {
        // Task 2: Start Your Coroutine Here
        StartCoroutine(SpawnRoutine());
	}

    // Task 2: Write Your Coroutine Here
	IEnumerator SpawnRoutine()
    {
        while (true) 
        {
            Instantiate(sentry, spawnPositions[Random.Range(0, spawnPositions.Length)]);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}

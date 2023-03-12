using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	bool moving;
	float movementDuration = 2.5f;

	// Use this for initialization
	void Start () {
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Don't use Update for this task
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Task 3: Start Your Coroutine Here
		if (!moving && other.tag == "Player")
        {
			moving = true;
			StartCoroutine(MoveRoutine(other.transform.position));
        }
    }

    // Task 3: Write Your Coroutine Here
	IEnumerator MoveRoutine(Vector3 playerPos) 
	{
		Vector3 initialPosition = transform.position;
		float elapsedTime = 0;
		while (elapsedTime < movementDuration)
		{
			Debug.Log(transform.position);
			transform.position = Vector3.Lerp(initialPosition, playerPos, elapsedTime/movementDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		moving = false;
	}
}

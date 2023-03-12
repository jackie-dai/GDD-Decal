using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody2D playerRigidbody;

    /* Task1 */
    private SpriteRenderer sp;
    private float fadeDuration = 1f;
	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        playerRigidbody.velocity = movement * 2;

        if(Input.GetKeyDown(KeyCode.F)) {
            // Task 1: Start Your Coroutine Here
            StartCoroutine(FadeToBlack());

        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall") {
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    // Task 1: Write Your Coroutine Here
    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            sp.color = Color.Lerp(Color.green, Color.black, fadeDuration / elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        sp.color = Color.green;
    }
}
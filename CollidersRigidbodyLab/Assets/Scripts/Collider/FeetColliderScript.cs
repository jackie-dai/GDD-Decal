﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetColliderScript : MonoBehaviour {

	private PlayerControllerTask2 player;

    void Awake()
    {
        player = GetComponentInParent<PlayerControllerTask2>();
    }
    // Returns whether the obj is a floor, platform, or wall
    bool isFloor(GameObject obj) {
		return obj.layer == LayerMask.NameToLayer ("Floor");
	}

    // use coll.gameObject if you need a reference coll's GameObject
	void OnCollisionEnter2D(Collision2D other) {
        //TASK 2
        player.ResetJump();
	}

	void OnCollisionExit2D(Collision2D other) {
        //TASK 2
        player.SetJumpToFalse();
    }

	//IF YOU NEED TO SET A PUBLIC VARIABLE IN A PARENT (hint hint)
	//GetComponentInParent<PlayerControllerTask2>().variable_name
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ParticleSystem))] // Used because I call GetComponent<ParticleSystem>()
public class MouseInput : MonoBehaviour
{
    #region Score Variables
    private int enemyValue = 100;
    private int bombValue = -100;
    #endregion
    #region Cached Components
    // The effects that should be played whenever the player clicks somewhere.
    private ParticleSystem m_Particles;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Particles = GetComponent<ParticleSystem>();
   }
   #endregion

   #region Main Updates
   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         wp.z = 0;

         ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
         emitParams.position = wp;
         emitParams.applyShapeToPosition = true;
         m_Particles.Emit(emitParams, 100);

         Collider2D[] allOverlaps = Physics2D.OverlapCircleAll(wp, 1.1f);
         Debug.Log(string.Format("Hit {0} objects", allOverlaps.Length));
         for (int i = 0; i < allOverlaps.Length; i++)
         {
            GameObject target = allOverlaps[i].gameObject;
            //Do your thing here.
            if (target.tag == "Fruit")
                {
                    Score.Singleton.AddScore(enemyValue);
                }
            if (target.tag == "Bomb")
                {
                    Score.Singleton.AddScore(bombValue);
                }
            Destroy(target);
         }
      }
   }
   #endregion
}

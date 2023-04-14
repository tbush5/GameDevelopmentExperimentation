using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostJumpTrigger : MonoBehaviour
{
    public float boostJump = 100;

    private float normalJump;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
            normalJump = playerObject.GetComponent<PlayerBehaviorShooting>().JumpVelocity;

    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviorShooting>().JumpVelocity = boostJump;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviorShooting>().JumpVelocity = normalJump;
        }
    }
}

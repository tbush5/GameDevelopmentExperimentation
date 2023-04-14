using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSppedBoost : MonoBehaviour
{
    private float timer;
    private bool superFast = false;
    private float origSpeed = 6f;

    private float boostDuration;
    private AudioSource audioPlayer = null;

    private string playerName = "";
    private PlayerBehaviorShooting script = null;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        audioPlayer = this.GetComponent<AudioSource>();

        GameObject playerObject = this.gameObject;

        if (playerObject != null)
            script = playerObject.GetComponentInChildren<PlayerBehaviorShooting>();

        if (script != null && playerObject != null){
            playerName = playerObject.name;
            origSpeed = script.MoveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (script != null && superFast && (timer > boostDuration))
        {
            superFast = false;
            script.MoveSpeed = origSpeed;
        }
    }

    public void OnCollisoinEnter( Collision collision )
    {
        Collider col = collision.collider;

        if (col.gameObject.tag == "CollisionObject")
            boostSpeed(col);
    }

    public void ontriggerEnter( Collider col )
    {
        if (col.gameObject.tag == "CollisionObject")
            boostSpeed(col);
    }

    private void boostSpeed(Collider col)
    {
        if (!script)
            return;

        AudioClip playSound = null;

        ObjectSpeedBoost objSpeedScript = col.gameObject.GetComponent<ObjectSpeedBoost>();

        if (objSpeedScript != null)
        {
            Debug.Log("Boosting speed");

            timer = 0.0f;

            float speedMultiplier = objSpeedScript.speedMultiplier;
            boostDuration = objSpeedScript.speedBoostDuration;

            script.MoveSpeed = script.MoveSpeed * speedMultiplier;

            if( objSpeedScript.destroyOnCollision)
                Destroy (col.gameObject);

            playSound = objSpeedScript.speedBoostSound;
        }

        if(playSound != null)
        {
            if (audioPlayer != null)
                audioPlayer.PlayOneShot(playSound);
        }
    }
}

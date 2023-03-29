using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooting : MonoBehaviour
{   
    private GameObject _player;

    public Rigidbody Bullet;
    public float BulletSpeed = 35f;
    private bool _isShooting;

    public AudioClip throwSound;

    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (_isShooting)
        {
            Rigidbody newBullet = Instantiate(Bullet, 
                        _player.transform.position + new Vector3(0, 0.75f, 0), 
                        this.transform.rotation * this.Bullet.transform.rotation);

            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), _player.gameObject.GetComponent<Collider>());
            
            newBullet.velocity = _player.transform.forward * BulletSpeed;

            if (throwSound != null)
            {
                AudioSource audioPlayer = newBullet.GetComponent<AudioSource>();

                if (audioPlayer != null)
                    audioPlayer.PlayOneShot(throwSound);
                else
                    Debug.LogWarning ("Your " + newBullet.gameObject.name + "  prefab must have an audio component");
            }
        }

        _isShooting = false;
    }
}

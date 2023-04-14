using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    private float _vInput;
    private float _hInput;

    public float jumpVelocity = 5f;
    private bool _isJumping;

    private Rigidbody _rb;

    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    public Rigidbody Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    public AudioClip throwSound;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * moveSpeed;
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        _isJumping |= Input.GetKeyDown(KeyCode.J);

        _isShooting |=  Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if(IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;

        if (_isShooting)
        {
            Rigidbody newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0.75f, 0), this.transform.rotation * this.Bullet.transform.rotation);
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
            newBullet.velocity = this.transform.forward * BulletSpeed;

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

    private bool IsGrounded()
        {
            Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
            bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
            return grounded;
        }
}

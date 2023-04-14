using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorShooting : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpVelocity = 5f;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;

    private Rigidbody _rb;
    private CapsuleCollider _col;
    private bool _isJumping;

    public bool underwater = false;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _isJumping |= Input.GetKeyDown(KeyCode.J);
    }

    // FixedUpdate is called once per frame at fixed intervals
    void FixedUpdate()
    {
        float _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        float _hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        // forward movement
        _rb.MovePosition (this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);

        // rotational movement
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler (rotation * Time.fixedDeltaTime);
        _rb.MoveRotation (_rb.rotation * angleRot);

        // jumping
        if( underwater && _isJumping){
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        else if ( IsGrounded() &&_isJumping ){
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }

        _isJumping = false;
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3 (_col.bounds.center.x,
                    _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule (_col.bounds.center,
                    capsuleBottom, DistanceToGround, GroundLayer,
                    QueryTriggerInteraction.Ignore);

        return grounded;
    }
}

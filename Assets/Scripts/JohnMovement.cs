using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private bool _grounded;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            _grounded = true;
        }
        else _grounded = false;
        

        if (Input.GetKeyDown(KeyCode.W) && _grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
    

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }
}

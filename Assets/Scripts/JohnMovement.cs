using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;
    public float jumpForce;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _horizontal;
    private bool _grounded;
    private float _lastShoot;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (_horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
        _animator.SetBool("running", _horizontal != 0.0f);
        
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

        if (Input.GetKey(KeyCode.Space) && Time.time > _lastShoot + 0.25f)
        {
            Shoot();
            _lastShoot = Time.time;
        }
    }
    

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
    

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }
}

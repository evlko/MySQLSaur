using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LevelManager levelManager;
    
    public float jumpVelocity;
    private bool _canJump;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _canJump = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _canJump = false;
            _rigidbody2D.velocity = new Vector2(0, jumpVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canJump = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            levelManager.Fail();
        }
    }
}

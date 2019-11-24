using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController _controller;
    private Transform _ground_check;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private float _gravity = -10f;

    [SerializeField]
    private float _ground_distance = 0.4f;
    
    [SerializeField]
    private LayerMask _ground_mask;

    [SerializeField]
    private bool _is_grounded;

    [SerializeField]
    float _jump_height = 3f;

    private Vector3 _velocity;
    
    void Start() {
        _controller = GetComponent<CharacterController>();
        _ground_check = transform.Find("GroundCheck");
    }

    void Update() {

        _is_grounded = Physics.CheckSphere(_ground_check.position, _ground_distance, _ground_mask);

        if(_is_grounded && _velocity.y < 0) {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && _is_grounded) {
            _velocity.y = Mathf.Sqrt(_jump_height * -2 * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

}

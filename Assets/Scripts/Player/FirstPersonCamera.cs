using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    private Transform _player_body;

    [SerializeField]
    private float _mouse_sensitivity = 100f;

    private float _rotation_x = 0f;

    void Start() {
        _player_body = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float mouse_x = Input.GetAxis("Mouse X") * _mouse_sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * _mouse_sensitivity * Time.deltaTime;

        _rotation_x -= mouse_y;
        _rotation_x = Mathf.Clamp(_rotation_x, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_rotation_x, 0f, 0f);
        _player_body.Rotate(Vector3.up * mouse_x);
    }
}

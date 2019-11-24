using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject {

    TrailRenderer _trail = null;

    private float _trail_time = 0f;

    [SerializeField]
    private int _speed = 0;

    void Awake() {
        _trail = GetComponent<TrailRenderer>();
        _trail_time = _trail.time; 
    }

    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    public override void OnObjectReuse() {
        _trail.time = -1;
        Invoke("ResetTrail", 0.1f);
        //transform.localScale = Vector3.one;
    }

    void ResetTrail() {
        _trail.time = _trail_time;
    }

}

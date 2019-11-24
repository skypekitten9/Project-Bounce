using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet = null;

    [SerializeField]
    private int _number_of_bullets = 0;

    void Start() {
        PoolManager.Instance.CreatePool(_bullet, _number_of_bullets);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PoolManager.Instance.ReUseObject(_bullet,Vector3.zero, Quaternion.identity);
        }
    }

}

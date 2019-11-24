using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    static PoolManager _instance;

    Dictionary<int, Queue<ObjectInstance>> _pool_dictionary = new Dictionary<int, Queue<ObjectInstance>>();

    public static PoolManager Instance {
        get {
            if(_instance == null) {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    public void CreatePool(GameObject prefab, int pool_size) {
        int pool_key = prefab.GetInstanceID();

        GameObject pool_holder = new GameObject(prefab.name + "pool");
        pool_holder.transform.parent = transform;

        if (!_pool_dictionary.ContainsKey(pool_key)) {
            _pool_dictionary.Add(pool_key, new Queue<ObjectInstance>());

            for (var i = 0u; i < pool_size; ++i) {
                ObjectInstance new_object = new ObjectInstance(Instantiate(prefab) as GameObject);
                _pool_dictionary[pool_key].Enqueue(new_object);
                new_object.SetParent(pool_holder.transform);
            }
        }
    }

    public void ReUseObject(GameObject prefab, Vector3 position, Quaternion rotation) {
        int poolkey = prefab.GetInstanceID();

        if (_pool_dictionary.ContainsKey(poolkey)) {
            ObjectInstance object_to_reuse = _pool_dictionary[poolkey].Dequeue();
            _pool_dictionary[poolkey].Enqueue(object_to_reuse);

            object_to_reuse.Reuse(position, rotation);
        }
    }

    public class ObjectInstance {
        GameObject _game_object;
        Transform _transform;

        bool _has_pool_object_component;
        PoolObject _pool_object_script;

        public ObjectInstance(GameObject object_instance) {
            _game_object = object_instance;
            _transform = _game_object.transform;
            _game_object.SetActive(false);

            if (_game_object.GetComponent<PoolObject>()) {
                _has_pool_object_component = true;
                _pool_object_script = _game_object.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation) {
            if (_has_pool_object_component) {
                _pool_object_script.OnObjectReuse();
            }
            _game_object.SetActive(true);
            _transform.position = position;
            _transform.rotation = rotation;
        }

        public void SetParent(Transform parent) {
           _transform.parent = parent;
        }

    }

}

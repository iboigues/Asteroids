using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Enemy Values")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _spawnRate = 10;
    [SerializeField] private int _spawnRateIncrement = 1;
    [SerializeField] private float _xLimit = 5.0f;

    private float _spawnNext = 0;

    void Start(){
        _xLimit = Camera.main.orthographicSize + 1;
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > _spawnNext){

            _spawnNext = Time.time + 60 / _spawnRate;

            _spawnRate += _spawnRateIncrement;

            float rand = Random.Range(-_xLimit,_xLimit);

            Vector2 pos = new Vector2(rand,8.0f);

            ObjectPool.Instance.SpawnFromPool("Meteor",pos,Quaternion.identity);
        }
    }
}

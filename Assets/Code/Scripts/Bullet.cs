using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField] private Rigidbody2D _body;

    [Header("Physic Values")]
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _lifetime = 4.0f;

    // Start is called before the first frame update
    void Start() {
        _body = GetComponent<Rigidbody2D>();
        Destroy(gameObject,_lifetime);
    }

    public void Shoot(Vector3 direction){
        _body.velocity = _speed * direction;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "Enemy")
            return;

            
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}

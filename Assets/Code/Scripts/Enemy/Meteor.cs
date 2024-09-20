using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour {

    private Rigidbody2D _body;

    [SerializeField] private bool _small;
    [SerializeField] private float _forceMagnitude = 3.0f;

    private void Awake(){
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Bullet>() == null)
            return;

        if(_small)
            return;

        Vector2 newPos = transform.position;
        newPos.x += 1;

        Vector2 izq = new Vector2(-1,-1).normalized;
        Vector2 der = new Vector2(1,-1).normalized;

        Meteor small1 = ObjectPool.Instance.SpawnFromPool<Meteor>("SmallMeteor",transform.position,Quaternion.identity);
        Meteor small2 = ObjectPool.Instance.SpawnFromPool<Meteor>("SmallMeteor",newPos,Quaternion.identity);

        small1._body.AddForce(izq * _forceMagnitude, ForceMode2D.Impulse);
        small2._body.AddForce(der * _forceMagnitude, ForceMode2D.Impulse);
    }
}

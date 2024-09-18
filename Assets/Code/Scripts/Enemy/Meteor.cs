using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour {

    [SerializeField] private bool _small;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "Bullet")
            return;

        if(_small)
            return;

        //TODO: Hacer la bisectriz y eso
        Vector2 newPos = transform.position;
        newPos.x += 1;

        ObjectPool.Instance.SpawnFromPool("SmallMeteor",transform.position,Quaternion.identity);
        ObjectPool.Instance.SpawnFromPool("SmallMeteor",newPos,Quaternion.identity);
    }
}

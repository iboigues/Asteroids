using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField] private Rigidbody2D _body;

    [Header("Physic Values")]
    [SerializeField] private float _speed = 10.0f;
    //[SerializeField] private float _lifetime = 4.0f;

    // Start is called before the first frame update
    void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector3 direction){
        _body.velocity = _speed * direction;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Meteor>() == null)
            return;

        IncreaseScore();

        gameObject.SetActive(false);
        collision.gameObject.SetActive(false);
        _body.velocity = Vector2.zero;
    }

    private void IncreaseScore(){
        PlayerController.score++;
        UpdateScore();
    }

    private void UpdateScore(){
        GameObject gameObject = GameObject.Find("Score");
        gameObject.GetComponent<Text>().text = "Puntos: " + PlayerController.score;
    }
}

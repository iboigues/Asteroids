using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D _body;
    [SerializeField] private GameObject _weapon;

    [Header("Player Values")]
    [SerializeField] private float _trustSpeed = 10.0f;
    [SerializeField] private float _rotationSpeed = 3.0f;
    private Vector2 _borderLimit = new Vector2(5.0f,5.0f);

    [Header("Weapon Values")]
    [SerializeField] private float _cooldown = 0.3f;
    private float _cooldownCounter = 0.0f;

    public static int score = 0; 
    
    // Start is called before the first frame update
    void Start() {
        _body = GetComponent<Rigidbody2D>();

        _borderLimit.x = Camera.main.orthographicSize + 1;
        _borderLimit.y = (Camera.main.orthographicSize + 1) * Screen.width / Screen.width;
    }

    // Update is called once per frame
    void Update() {
        Shoot();
        InfiniteSpace();
    }

    void FixedUpdate(){
        Move();
    }

    private void Move(){
        float thrust = Input.GetAxis("Thrust");
        float rotation = Input.GetAxis("Rotate");

        Vector2 thrustDirection = transform.right;

        _body.AddForce(thrust * thrustDirection * _trustSpeed);

        transform.Rotate(Vector3.forward, -rotation * _rotationSpeed);
    }

    private void Shoot(){
        if(_cooldownCounter > 0.0f)    
            _cooldownCounter -= Time.deltaTime;


        if(Input.GetKey(KeyCode.Space) && _cooldownCounter <= 0.0f) {

            Bullet bullet = ObjectPool.Instance.SpawnFromPool<Bullet>("Bullet",_weapon.transform.position,Quaternion.identity);

            if(bullet == null){
                Debug.LogError("No disparo");
                return;
            }

            bullet.Shoot(_weapon.transform.right);
            
            _cooldownCounter = _cooldown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Meteor>() == null)
            return;


        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void InfiniteSpace(){
        Vector2 pos = transform.position;

        if(pos.x > _borderLimit.x)
            pos.x = -_borderLimit.x + 1;
        else if(pos.x < -_borderLimit.x)
            pos.x = _borderLimit.x - 1;
        else if(pos.y > _borderLimit.y)
            pos.y = -_borderLimit.y + 1;
        else if(pos.y < -_borderLimit.y)
            pos.y = _borderLimit.y - 1;

        transform.position = pos;
    }
}

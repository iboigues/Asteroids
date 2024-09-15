using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D _body;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Bullet _bullet;

    [Header("Player Values")]
    [SerializeField] private float _trustSpeed = 10.0f;
    [SerializeField] private float _rotationSpeed = 3.0f;

    [Header("Weapon Values")]
    [SerializeField] private float _cooldown = 0.5f;
    private float _cooldownCounter = 0.0f;

    public static int score = 0; 
    
    // Start is called before the first frame update
    void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Shoot();
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
            Bullet bullet = Instantiate(_bullet,_weapon.transform.position,Quaternion.identity);

            bullet.Shoot(_weapon.transform.right);
            
            _cooldownCounter = _cooldown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "Enemy")
            return;


        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

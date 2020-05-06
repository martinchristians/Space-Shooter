using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        // ≡ public float speed = 3.5f; -> with this player cant change the speed rate
    [SerializeField] // -> the value can be changed while play the game, however other Obj in script can't touch the value
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0,0,0)
        //transform.rotation
        //transform.position.y
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is null");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //move Player 1 meter per second
            //transform.Translate(Vector3.right * horizontalInput *  _speed * Time.deltaTime );
            //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //restraining bounds
            // if (transform.position.y >= 0)
            // {
            //     transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            // }
            // else if (transform.position.y <= -3.8f)
            // {
            //     transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);
            // }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), transform.position.z);

        //wrap the player and make it appear on the other side
        if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        //Debug.Log("Space Key pressed"); -> print the massage
        
        _canFire = Time.time + _fireRate;
        
        //make input through press of the keyboard
            // without new Vector3(0,0.8f,0) ≡ by Start() in Laser.cs write down: transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Instantiate(_laserPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity); // Quaternion.identity -> default rotation
    }

    public void Damage()
    {
        _lives -= 1;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
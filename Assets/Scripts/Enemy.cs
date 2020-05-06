using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -6.0f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 8.0f, transform.position.z);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
            //Debug.Log("Hit: " + other.transform.name); -> 'other' give us Obj; 'transform' give us route of the Obj; 'name' give us name of the Obj
        if (other.tag == "Player")
        {
                //other.transform.GetComponent<Player>().Damage(); -> possible only write this, but error prone
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
        
        if (other.tag == "Laser") 
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

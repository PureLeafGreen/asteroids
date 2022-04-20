using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brigand : MonoBehaviour
{
    public float movementSpeed = 3f, rotationSpeed = 125f;
    public float health = 3;
    public int ammo = 10;

    public GameObject missile, canon;
    public GameObject explosion;
    public Transform player;
    float range = 10f;
    float time = 0f;
    float interval = 10f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);
        if (distance > range)
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
        
        else if (distance <= range)
        {
            Aim();
            time += Time.deltaTime;

            if (time >= interval) {
                time = 0.0f;
                Fire();
            }
        }
    }

    private void Fire()
    {
        if (ammo >= 1)
        {
            ammo--;
            Instantiate(missile, transform.position, transform.rotation);
        }
    }

    private void Aim()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            if (health > 1)
            {
                health = health - 1;
            }
            if (health == 0) { 
                Instantiate(explosion, explosion.transform.position, explosion.transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

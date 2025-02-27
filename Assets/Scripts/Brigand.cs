using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brigand : MonoBehaviour
{
    public float movementSpeed = 3f, rotationSpeed = 125f;
    public float health = 3f;
    public int ammo = 10;
    int nbPoint = 5;

    public GameObject missile, canon;
    public GameObject explosion;
    public Transform player;
    public GameObject[] asteroids;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, -9, 9);
        newPos.y = Mathf.Clamp(newPos.y, -5, 5);
        transform.position = newPos;


        var posititionAsteroid = GetClosestEnemy(asteroids);
        var distanceAsteroid = Vector2.Distance(posititionAsteroid.position, transform.position); 
        var distance = Vector2.Distance(player.position, transform.position);
        if (distance >= distanceAsteroid)
        {
            Dodge(posititionAsteroid);
            transform.position += transform.up * movementSpeed * Time.deltaTime;

        }
        else
        {
            Aim();
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (ammo >= 1)
        {
            ammo--;
            Instantiate(missile, canon.transform.position, canon.transform.rotation);
        }
    }

    private void Dodge(Transform asteroid)
    {
        Vector2 direction = asteroid.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -180;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Aim()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void Toucher()
    {
        if (health > 1 )
        {
            health--;
        }
        else
        {
            ScoreManager.Instance.AddScore(nbPoint);
            Destroy(gameObject);
        }
    }
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject); // Detruire le missile
            Instantiate(explosion, other.transform.position, other.transform.rotation); // Creer une explosion

            other.transform.GetComponent<Asteroid>()?.Explode();
        }
        if (other.CompareTag("Tirailleur"))
        { // Detruire le missile
            Instantiate(explosion, transform.position, transform.rotation); // Creer une explosion
            Destroy(gameObject);
            other.transform.GetComponent<Tirailleur>()?.Toucher();
        }
    }
}

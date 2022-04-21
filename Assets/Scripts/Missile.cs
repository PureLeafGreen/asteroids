using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    
    public float movementSpeed;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, movementSpeed * Time.deltaTime, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject); // Detruire le missile
            Instantiate(explosion, other.transform.position, other.transform.rotation); // Creer une explosion

            other.transform.GetComponent<Asteroid>()?.Explode();
        }
        if (other.CompareTag("Brigand"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);  // Creer une explosion
            other.transform.GetComponent<Brigand>()?.Toucher();
        }
        if (other.CompareTag("Tirailleur"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);  // Creer une explosion
            other.transform.GetComponent<Tirailleur>()?.Toucher();
        }
        if (other.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);  // Creer une explosion
            other.transform.GetComponent<Player>()?.Toucher();
        }
    }
}

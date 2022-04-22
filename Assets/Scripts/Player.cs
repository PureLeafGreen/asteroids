using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f, rotationSpeed = 150f;
    float tmpSpeed = 5f;
    float timeBoost = 10f;
    float timeForBoost = 0f;

    public GameObject missile, canon;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, 0, -rotation * Time.deltaTime);

        float translation = Input.GetAxis("Vertical") * movementSpeed;
        transform.Translate(0, translation*Time.deltaTime, 0, Space.Self);

        var newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, -9, 9);
        newPos.y = Mathf.Clamp(newPos.y, -5, 5);
        transform.position = newPos;

        if (Input.GetKeyDown("space"))
        {
            Instantiate(missile, canon.transform.position, canon.transform.rotation);
        }
        if (movementSpeed > tmpSpeed)
        {
            timeForBoost += Time.deltaTime;
            if (timeForBoost >= timeBoost)
            {
                movementSpeed = tmpSpeed;
            }
        }
    }


    public void upgradeMouvementSpeed(float speed) {
        movementSpeed = speed;
    }

    public void OnTriggerEnter{
        if (other.CompareTag("Asteroid"))
        {
            Instantiate(explosion, transform.position, transform.rotation); // Creer une explosion
            Toucher();
            other.transform.GetComponent<Asteroid>()?.Explode();
        }
        if (other.CompareTag("Brigand"))
        { // Detruire le missile
            Instantiate(explosion, transform.position, transform.rotation); // Creer une explosion
            Toucher();
            other.transform.GetComponent<Brigand>()?.Toucher();
        }
    }

    public void Toucher()
    {
        Destroy(gameObject);
        ScoreManager.Instance.OnDestroy();
    }
}

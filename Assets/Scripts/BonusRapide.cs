using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRapide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            var player = other.GetComponent<Player>();
            player.upgradeMouvementSpeed(15);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().pickUpCoins();
        AudioSource.PlayClipAtPoint(coinPickSFX, Camera.main.transform.position);
        
    }
}

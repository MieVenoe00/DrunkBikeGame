using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieDieDie : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("car"))
    {
        Die();
        Debug.Log("Du er død");
    }
}

    void Die()
    {
        
        Destroy(gameObject);
    }
}

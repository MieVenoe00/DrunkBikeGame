using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieDieDie : MonoBehaviour
{
  
    public GameObject DødMand;

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
        Instantiate(DødMand, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }
}

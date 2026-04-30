using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieDieDie : MonoBehaviour
{
  
    public GameObject DødMand;
    public GameObject replayCanvas; 
    public GameObject spawner;       

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
        // Stop spawner
        if (spawner != null)
            spawner.SetActive(false);

        // Vis replay knap
        if (replayCanvas != null)
            replayCanvas.SetActive(true);
            
        Instantiate(DødMand, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }
}

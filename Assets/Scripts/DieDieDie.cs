using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieDieDie : MonoBehaviour
{
  
    public GameObject DødMand;
    public GameObject replayCanvas; 
    public GameObject spawner;     

      [Header("Animationer der stopper ved død")]
    public Animator[] animatorsToDisable;
  

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

        // Stop animationer
        foreach (Animator anim in animatorsToDisable)
        {
            anim.enabled = false;
        }

        Instantiate(DødMand, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }
}

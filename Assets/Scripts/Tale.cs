using UnityEngine;
using System.Collections;


public class Tale : MonoBehaviour
{
    public GameObject speechBubblePrefab;

    void Start()
    {
        GameObject bubble = Instantiate(speechBubblePrefab);

        Destroy(bubble, 3f); 
    }
}

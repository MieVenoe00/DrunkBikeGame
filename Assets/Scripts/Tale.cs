using UnityEngine;
using System.Collections;
public class Tale : MonoBehaviour
{
    public GameObject speechBubble;

    IEnumerator Start()
    {
        speechBubble.SetActive(true);

        yield return new WaitForSeconds(3f);

        speechBubble.SetActive(false);
    }
}

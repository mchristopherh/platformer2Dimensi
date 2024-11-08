using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowVictoryCo());
            
        }
    }

    public IEnumerator ShowVictoryCo()
    {
        yield return new WaitForSeconds(0.001f);
        if (UIController.instance != null){
            UIController.instance.ShowVictory();
        }
    }
}

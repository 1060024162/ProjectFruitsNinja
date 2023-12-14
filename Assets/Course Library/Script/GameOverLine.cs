using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    
    [SerializeField] GameManager gameManager;

  

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("trigger");*/
        if (other.tag != "Bad")
        {
            gameManager.UpdateLife();
        }
    }
}

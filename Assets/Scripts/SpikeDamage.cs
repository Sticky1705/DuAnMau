using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject whatHit = col.gameObject;
        if(whatHit.CompareTag("Player"))
        {
            //Health.health -= 30f;
        }
    }
}

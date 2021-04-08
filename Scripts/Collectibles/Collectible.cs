using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] CollectHealth collectHealth;
    [SerializeField] CollectScore collectPoints;

    [SerializeField] private bool isHeal;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isHeal && collectHealth != null)
            {
                collectHealth.DoCollect();
            }
            else if (!isHeal && collectPoints != null)
            {
                collectPoints.DoCollect();
            }
            Destroy(gameObject);
        }
    }
}

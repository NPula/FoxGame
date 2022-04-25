using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.amountOfFood++;
            Destroy(gameObject);

            Debug.Log("Food: " + PlayerManager.Instance.amountOfFood);
        }
    }
}

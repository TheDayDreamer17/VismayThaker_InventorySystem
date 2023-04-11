using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Autovrse
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> inventoryItems;
        private void OnEnable()
        {
            GameEvents.OnEnemyDie += OnEnemyDie;
        }
        private void OnDisable()
        {
            GameEvents.OnEnemyDie -= OnEnemyDie;
        }

        private void OnEnemyDie(Vector3 positionOfDeath)
        {
            GameObject inventoryItem = inventoryItems[Random.Range(0, inventoryItems.Count)];
            Instantiate(inventoryItem, positionOfDeath, inventoryItem.transform.rotation, transform);
        }
    }

}

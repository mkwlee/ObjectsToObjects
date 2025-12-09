using System.Collections.Generic;
using SpaceGame;
using TMPro;
using UnityEngine;

namespace SpaceGame
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private PickUpSpawn[] pickups;
        [Range(0, 1)][SerializeField] private float pickupProbability;

        List<PickUp> pickupPool = new List<PickUp>();
        PickUp chosenPickup;

        void Start()
        {
            foreach (PickUpSpawn spawn in pickups)
            {
                for(int i = 0; i < spawn.spawnWeight; i++)
                {
                    pickupPool.Add(spawn.pickup);
                }
            }
        }

        public void SpawnPickup(Vector2 position)
        {
            if (pickupPool.Count <= 0)
                return;
            
            if (Random.Range(0.01f, 1.0f) < pickupProbability)
            {
                chosenPickup = pickupPool[Random.Range(0, pickupPool.Count)];
                Instantiate(chosenPickup, position, Quaternion.identity);
            }
        }
    }
}

[System.Serializable]
public struct PickUpSpawn
{
    public PickUp pickup;
    public int spawnWeight;
}
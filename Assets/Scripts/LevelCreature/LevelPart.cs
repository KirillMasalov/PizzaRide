using System;
using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelCreature
{
    public class LevelPart : MonoBehaviour
    {
        [field: SerializeField] public LevelExitDirection ExitDirection { get; set; }

        [Space(10)] 
        [Header("EnterNodes")] 
        [SerializeField] private CarMovementNode straightInnerEnter;
        [SerializeField] private CarMovementNode straightOuterEnter;
        [SerializeField] private CarMovementNode reverseOuterEnter;
        [SerializeField] private CarMovementNode reverseInnerEnter;
        

        [Space(10)] 
        [Header("ExitNodes")] 
        [SerializeField] private CarMovementNode straightInnerExit;
        [SerializeField] private CarMovementNode straightOuterExit;
        [SerializeField] private CarMovementNode reverseOuterExit;
        [SerializeField] private CarMovementNode reverseInnerExit;
        
        private LevelPartSpawnCars spawnCars;
        private LevelPartCoinsSpawn spawnCoins;

        private void Awake()
        {
            spawnCars = GetComponent<LevelPartSpawnCars>();
            if (spawnCars is null)
                throw new NullReferenceException("LevelPart | SpawnCars component is null");
            
            spawnCoins = GetComponent<LevelPartCoinsSpawn>();
            if (spawnCoins is null)
                throw new NullReferenceException("LevelPart | SpawnCoins component is null");
        }

        public void ConnectToOldPart(LevelPart otherPart)
        {
            otherPart.straightOuterExit.NextNode = straightOuterEnter;
            otherPart.straightInnerExit.NextNode = straightInnerEnter;
            reverseOuterExit.NextNode = otherPart.reverseOuterEnter;
            reverseInnerExit.NextNode = otherPart.reverseInnerEnter;
        }

        public void SpawnCars()
        {
            spawnCars.SpawnAllCars();
        }

        public void SpawnCoins()
        {
            spawnCoins.SpawnAllCoins();
        }

        public void ReturnCoins()
        {
            spawnCoins.ReturnCoinsToPool();
        }
    }
}

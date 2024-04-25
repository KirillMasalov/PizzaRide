using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreature
{
    public class LevelController : MonoBehaviour
    {
        private const int MAX_PARTS_QUEUE_LENGTH = 5;

        [SerializeField] private int levelPartsCountToFinish;
        [SerializeField] private Transform levelWall;
        [SerializeField] private LevelPart startPart;
        [SerializeField] private LevelPart firstPart;
        public static LevelController Instance { get; private set; }
        
        private Queue<LevelPart> levelParts = new ();
        private LevelPart currentHeadPart;
        private float currentDirection = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            levelParts.Enqueue(startPart);
            levelParts.Enqueue(firstPart);
            currentHeadPart = firstPart;
            LevelBuilder.Instance.Initialize(new HashSet<Vector3>{
                startPart.transform.position.RoundToInt(),
                firstPart.transform.position.RoundToInt()});
        }

        public void AddLevelPart()
        {
            LevelPart newPart = null;
            if (levelPartsCountToFinish > 0)
                newPart = LevelBuilder.Instance.CreateNextPart(currentHeadPart, currentDirection);
            else
                newPart = LevelBuilder.Instance.CreateFinishPart(currentHeadPart, currentDirection);
            levelPartsCountToFinish--;
            
            if (newPart is null)
                throw new NullReferenceException("LevelController | New level part is null");
            
            levelParts.Enqueue(newPart);
            newPart.ConnectToOldPart(currentHeadPart);
            newPart.SpawnCars();
            newPart.SpawnCoins();
            
            currentDirection = (currentDirection + (int)newPart.ExitDirection) % 360;
            currentHeadPart = newPart;

            if (levelParts.Count > MAX_PARTS_QUEUE_LENGTH)
                DeleteTailPart();
            
        }

        private void DeleteTailPart()
        {
            var tailPart = levelParts.Dequeue();
            tailPart.ReturnCoins();
            
            var lastPartPosition = tailPart.transform.position;
            LevelBuilder.Instance.RemoveOccupiedPosition(lastPartPosition);
            
            Destroy(tailPart.gameObject);
            levelWall.position = lastPartPosition;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace LevelCreature
{
    public class LevelBuilder: MonoBehaviour
    {
        public static LevelBuilder Instance { get; private set; }
        
        private const int SAFE_COUNTER = 10;
        
        [SerializeField] private List<LevelPart> levelPartPrefabs;
        [SerializeField] private int levelPartSize = 300;
        [SerializeField] private LevelPart primitivePartPrefab;
        [SerializeField] private LevelPart finishPartPrefab;
        private HashSet<Vector3> occupiedPositions;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(HashSet<Vector3> startOccupiedPositions)
        {
            occupiedPositions = startOccupiedPositions;
        }

        public LevelPart CreateNextPart(LevelPart currentHeadPart, float currentDirection)
        {
            var nextPartPosition = GetNextPartPos(currentHeadPart.transform.position, currentDirection);
            
            var nextPartPrefab = ChooseRandomPart();
            var predictPos = GetNextPartPos(nextPartPosition, (currentDirection 
                                                               + (int)nextPartPrefab.ExitDirection) % 360);
            var safeCounter = SAFE_COUNTER;
            while (!PositionEmpty(predictPos) && safeCounter >= 0)
            {
                nextPartPrefab = (safeCounter == 0)?ChooseRandomPart():primitivePartPrefab;
                predictPos = GetNextPartPos(nextPartPosition, (currentDirection 
                                                               + (int)nextPartPrefab.ExitDirection) % 360);
                safeCounter--;
            }

            return InstantiatePart(nextPartPrefab, nextPartPosition, currentDirection);
        }

        public LevelPart CreateFinishPart(LevelPart currentHeadPart, float currentDirection)
        {
            var nextPartPosition = GetNextPartPos(currentHeadPart.transform.position, currentDirection);
            return InstantiatePart(finishPartPrefab, nextPartPosition, currentDirection);
        }

        private LevelPart InstantiatePart(LevelPart nextPartPrefab, Vector3 nextPartPosition, float currentDirection)
        {
            var nextPartTransform = Instantiate(nextPartPrefab.gameObject).transform;
            nextPartTransform.position = nextPartPosition;
            nextPartTransform.eulerAngles = Vector3.up * currentDirection;
            AddOccupiedPosition(nextPartPosition);

            return nextPartTransform.GetComponent<LevelPart>();
        }

        public void RemoveOccupiedPosition(Vector3 pos)
        {
            occupiedPositions.Remove(pos);
        }

        private void AddOccupiedPosition(Vector3 pos)
        {
            occupiedPositions.Add(pos.RoundToInt());
        }

        private LevelPart ChooseRandomPart()
        {
            return levelPartPrefabs[UnityEngine.Random.Range(0, levelPartPrefabs.Count)];
        }

        private Vector3 GetNextPartPos(Vector3 initialPos, float direction)
        {
            return (initialPos + Quaternion.Euler(0, direction, 0) * Vector3.forward * levelPartSize).RoundToInt();
        }

        private bool PositionEmpty(Vector3 pos)
        {
            return !occupiedPositions.Contains(pos.RoundToInt());
        }
    }
}

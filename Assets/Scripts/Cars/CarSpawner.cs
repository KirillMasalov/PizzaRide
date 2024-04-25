using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class CarSpawner : MonoBehaviour
    {
        public static CarSpawner Instance { get; private set; }

        [SerializeField] private List<Car> carsPrefabs;
        [SerializeField] private EmptyBoxChecker checkEmptyBox;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnCar(CarSpawnPoint spawnPoint)
        {
            var spawnPointTransform = spawnPoint.transform;
            if (!SpawnPointIsFree(spawnPointTransform))
            {
                return;
            }

            var carPrefab = ChooseRandomCar();
            var carTransform = Instantiate(carPrefab).transform;
            var car = carTransform.GetComponent<Car>();
            
            carTransform.position = spawnPointTransform.position;
            carTransform.eulerAngles = spawnPointTransform.eulerAngles;
            car.Initialize(spawnPoint.StartTargetNode);
        }

        private bool SpawnPointIsFree(Transform spawnPoint)
        {
            return checkEmptyBox.Check(spawnPoint.position);
        }

        private Car ChooseRandomCar()
        {
            return carsPrefabs[Random.Range(0, carsPrefabs.Count)];
        }
    }
}

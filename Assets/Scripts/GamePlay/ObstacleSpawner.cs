using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ML.RootShift;
using ML.GameEvents;

namespace ML.GamePlay
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onGameOver;
        [SerializeField] VoidListener onPlayAgain;
        [SerializeField] float spawnDistance = 15;
        [SerializeField] Transform[] spawnPoints = new Transform[0];
        [SerializeField] GameObject[] obstacels = new GameObject[0];
    
        ObjectPool<GameObject> obstaclePool;
        List<GameObject> spawnedObstacles = new List<GameObject>();
        float acumulatedDistance;
        float lastPosition;
        bool isPlaying = false;

        private void Awake()
        {
            obstaclePool = new ObjectPool<GameObject>(
                HandleCreateObstacle,
                HandleGetObstacle,
                HandleReleaseObstace,
                HandleDestroyObstacle,
                false,
                0, 
                100);

            onGameStart.onGameEventInvoke += HandleGameStart;
            onGameOver.onGameEventInvoke += HandleGameOver;
            onPlayAgain.onGameEventInvoke += HandlePlayAgain;

            onGameStart.HookToGameEvent();
            onGameOver.HookToGameEvent();
            onPlayAgain.HookToGameEvent();
        }

        

        private void HandleDestroyObstacle(GameObject obstacle)
        {
            Destroy(obstacle);
        }

        private void HandleReleaseObstace(GameObject obstacle)
        {
            obstacle.SetActive(false);
        }

        private void HandleGetObstacle(GameObject obstacle)
        {
            Transform spawnPoint = GetSpawnPont();
            obstacle.transform.position = spawnPoint.position;
            obstacle.SetActive(true);
        }

        private GameObject HandleCreateObstacle()
        {
            GameObject obstacleToSpawn = GetObstacle();
            GameObject spawnedObstacle = Instantiate(obstacleToSpawn, RootShiftManager.Root);
            if (spawnedObstacle.TryGetComponent<IPoolable>(out IPoolable poolable))
            {
                poolable.onRelease += (obj) => obstaclePool.Release(obj);
            }
            spawnedObstacles.Add(spawnedObstacle);
            return spawnedObstacle;
        }

        private void OnDestroy()
        {
            onGameStart.UnHookFromGameEvent();
            onGameOver.UnHookFromGameEvent();
            onPlayAgain.UnHookFromGameEvent();
            obstaclePool.Dispose();
        }

        private void Update()
        {
            if (CanSpawnNext())
            {
                obstaclePool.Get();
                //Transform spawnPoint = GetSpawnPont();
                //GameObject obstacle = GetObstacle();
                //Instantiate(obstacle, spawnPoint.transform.position, spawnPoint.transform.rotation, RootShiftManager.Root);
            }
        }

        Transform GetSpawnPont()
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnPointIndex];
            return spawnPoint;
        }


        GameObject GetObstacle()
        {
            int obstacleIndex = Random.Range(0, obstacels.Length);
            GameObject obstacle = obstacels[obstacleIndex];
            return obstacle;
        }

        bool CanSpawnNext()
        {
            if (!isPlaying)
                return false;


            float deltaDistance = transform.position.ShiftedToReal().z - lastPosition;
            lastPosition = transform.position.ShiftedToReal().z;

            acumulatedDistance += deltaDistance;

            if (acumulatedDistance >= spawnDistance)
            {
                acumulatedDistance -= spawnDistance;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HandleGameOver(Void obj)
        {
            isPlaying = false;
            acumulatedDistance = 0;
            lastPosition = 0;
        }

        private void HandleGameStart(Void obj)
        {
            isPlaying = true;
        }
        private void HandlePlayAgain(Void obj)
        {
            foreach(GameObject obstacle in spawnedObstacles)
            {
                obstaclePool.Release(obstacle);
            }    
        }
    }

    class WeightedRandom
    {
        public float[] Weights;

        WeightedRandom(int weights)
        {
            Weights = new float[weights];
            for (int i = 0; i < weights; i++)
            {
                Weights[i] = 1;
            }
        }

        public void Normalize()
        {
            float sum = Sum();
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] /= sum;
            }
        }

        float Sum()
        {
            float sum = 0;
            foreach (float w in Weights)
                sum += w;

            return sum;
        }
        public int Random()
        {
            float sum = Sum();
            float random = UnityEngine.Random.Range(0, sum);
            int index = 0;
            float currentSum = 0;
            foreach(float w in Weights)
            {
                currentSum += w;
                if (currentSum >= random)
                    return index;

                index++;
            }
            return 0;
        }

        public void ModifyWeight(int index, float modifyVal)
        {
            if (!(index >= 0 && index < Weights.Length))
                return;

            float min = 0.01f;
            float weight = Weights[index];
            weight = Mathf.Max(min, weight + modifyVal);
            Weights[index] = weight;

            Normalize();
        }
    }
}

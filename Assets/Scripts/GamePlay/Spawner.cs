using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ML.RootShift;
using ML.GameEvents;

namespace ML.GamePlay
{
    public class Spawner : MonoBehaviour
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
                HandleCreate,
                HandleGet,
                HandleRelease,
                HandleDestroy,
                true,
                2, 
                100);

            onGameStart.onGameEventInvoke += HandleGameStart;
            onGameOver.onGameEventInvoke += HandleGameOver;
            onPlayAgain.onGameEventInvoke += HandlePlayAgain;

            onGameStart.HookToGameEvent();
            onGameOver.HookToGameEvent();
            onPlayAgain.HookToGameEvent();
        }

        

        private void HandleDestroy(GameObject obstacle)
        {
            //Debug.Log("DESTROY", obstacle);
            //Debug.Break();
            Destroy(obstacle);
        }

        private void HandleRelease(GameObject obstacle)
        {
            obstacle.SetActive(false);
            spawnedObstacles.Remove(obstacle);
        }

        private void HandleGet(GameObject obstacle)
        {
            Transform spawnPoint = GetSpawnPont();
            obstacle.transform.position = spawnPoint.position;
            obstacle.SetActive(true);
            if (obstacle.TryGetComponent(out IPoolable poolable))
            {
                poolable.onGet?.Invoke();
            }
            spawnedObstacles.Add(obstacle);
        }

        private GameObject HandleCreate()
        {
            GameObject obstacleToSpawn = GetObstacle();
            GameObject spawnedObstacle = Instantiate(obstacleToSpawn, RootShiftManager.Root);
            if (spawnedObstacle.TryGetComponent<IPoolable>(out IPoolable poolable))
            {
                poolable.onRelease += (obj) => obstaclePool.Release(obj);
            }
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
            for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
            {

                obstaclePool.Release(spawnedObstacles[i]);
            }
        }
    }

}

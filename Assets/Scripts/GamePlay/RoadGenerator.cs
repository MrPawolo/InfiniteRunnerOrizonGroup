using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ML.RootShift;
using ML.GameEvents;

namespace ML.GamePlay
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] VoidListener onPlayAgain;
        [SerializeField] GameObject worldPartPrefab;
        [SerializeField] float worldPartLength = 40f;
        [SerializeField] float spawnDistance = 200;
        [SerializeField] float despawnDistance = 50;

        float lastSpawnNumber = -1;
        Queue<GameObject> spawnedParts = new Queue<GameObject>();
        ObjectPool<GameObject> partPool;
        Transform cameraTransfomr;
        void Awake()
        {
            partPool = new ObjectPool<GameObject>(
                HandleCreateWorldPart, 
                HandleOnGetWorldPartm, 
                HandleOnReleaseWorldPart, 
                HandleOnDestroyWorldPart, 
                false, 
                0,
                (int)(spawnDistance / worldPartLength) + 2);

            Prewarm();
            cameraTransfomr = Camera.main.transform;
            onPlayAgain.onGameEventInvoke += HandlePlayAgain;
            onPlayAgain.HookToGameEvent();
        }

        private void HandleOnDestroyWorldPart(GameObject part)
        {
            Destroy(part);
        }

        private void HandleOnReleaseWorldPart(GameObject part)
        {
            part.SetActive(false);
        }

        private void HandleOnGetWorldPartm(GameObject part)
        {
            Vector3 nextPoint = Vector3.forward * lastSpawnNumber * worldPartLength;
            part.transform.position = nextPoint.RealToShifted();
            part.SetActive(true);
            spawnedParts.Enqueue(part);
            lastSpawnNumber++;

        }

        private GameObject HandleCreateWorldPart()
        {
            GameObject spawnedObject = Instantiate(worldPartPrefab, RootShiftManager.Root);

            return spawnedObject;
        }

        private void OnDestroy()
        {
            partPool.Dispose();
            onPlayAgain.UnHookFromGameEvent();
        }


        private void HandlePlayAgain(GameEvents.Void obj)
        {
            lastSpawnNumber = -1;
            Prewarm();
        }

        public void Prewarm()
        {
            while(spawnedParts.Count > 0)
            {
                partPool.Release(spawnedParts.Dequeue());
            }

            while (lastSpawnNumber * worldPartLength <= spawnDistance)
            {
                partPool.Get();
            }
        }

        private void Update()
        {
            TrySpawnWorldPart();
        }

        void TrySpawnWorldPart()
        {
            GameObject gameObject = spawnedParts.Peek();
            if (Vector3.Distance(gameObject.transform.position, cameraTransfomr.position) > despawnDistance)
            {
                partPool.Release(spawnedParts.Dequeue());
                partPool.Get();
            }
        }
    }
}

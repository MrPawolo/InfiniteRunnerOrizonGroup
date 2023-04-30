using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class CarNormalizedScreenPos : MonoBehaviour
    {
        [SerializeField] float roadWidth = 12f;
        [SerializeField] float carWidth = 0.5f;
        [SerializeField] Transform trackTransform;

        Camera cam;
        float depth;
        public void Awake()
        {
            cam = Camera.main;
            depth = cam.WorldToScreenPoint(trackTransform.position).z;
        }

        public float GetNormalizedScreenXPos()
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(trackTransform.position);
            float normalizedXScreenPos = screenPosition.x / Screen.width;
            return normalizedXScreenPos;
        }

        public void SetNormalizedScreenXPos(float position)
        {
            Vector3 newWorldPos = cam.ScreenToWorldPoint(new Vector3(position * Screen.width, 0,depth) );
            Vector3 worldPos = trackTransform.position;
            worldPos.x = newWorldPos.x;
            worldPos.x = Mathf.Clamp(worldPos.x, -roadWidth / 2 + carWidth / 2, roadWidth / 2 - carWidth / 2);
            trackTransform.position = worldPos;

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.GamePlay
{
    [CreateAssetMenu(fileName = "NewPowerUp", menuName = "ML/PowerUp")]
    public class PowerUpContainer : ScriptableObject
    {
        [field: SerializeField] public GameObject PowerUpPrefab { get; private set; }
    }
}

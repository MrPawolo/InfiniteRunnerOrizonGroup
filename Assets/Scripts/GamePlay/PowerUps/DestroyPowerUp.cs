using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.GamePlay
{
    public class DestroyPowerUp : MonoBehaviour
    {
        public void TryDestroyPowerUp()
        {
            IPowerUp powerUp = GetComponentInChildren<IPowerUp>();
            if (powerUp == null)
                return;
            
            Destroy(powerUp.GetGameObject());
        }
    }
}

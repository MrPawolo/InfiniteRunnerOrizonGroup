using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void Instantiate()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInstantiator : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("InstantiatePrefab")]
    public void InstantiatePrefab()
    {
        Instantiate(_prefab, _position, Quaternion.identity);
    }
}

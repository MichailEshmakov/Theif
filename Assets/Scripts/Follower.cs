using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _followed;
    
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _followed.position;
    }

    void LateUpdate()
    {
        transform.position = _offset + _followed.position;
    }
}

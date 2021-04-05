using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class AnimationSetter : MonoBehaviour
{
    private Mover _mover;
    private Animator _animator;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponentInChildren<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Отстутсвует аниматор!");
        }
    }

    private void Update()
    {
        if (_animator != null)
        {
            _animator.SetFloat("speed", _mover.Speed / _mover.MaxSpeed);
        }
    }
}

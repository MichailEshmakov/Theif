using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class StairClimber : MonoBehaviour
{
    [SerializeField] private Transform _bottom;
    [SerializeField] private float _maxStepHeight;
    [SerializeField] private float _checkingDistance;
    [SerializeField] private float _climpingSpeed;

    private Rigidbody _rigidBody;
    private Mover _mover;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        ClimbUp();
    }

    private void ClimbUp()
    {
        Vector3 rightCheckingDirection = new Vector3(1, 0, 1);
        Vector3 leftCheckingDirection = new Vector3(-1, 0, 1);

        if (IsStairsCollided(Vector3.forward) || IsStairsCollided(rightCheckingDirection) || IsStairsCollided(leftCheckingDirection))
        {
            if (_mover.Speed > 0)
            {
                _rigidBody.position += new Vector3(0f, _climpingSpeed * Time.deltaTime, 0f);
            }
        }
    }

    private bool IsStairsCollided(Vector3 direction)
    {
        bool isSomthingAtBottom = Physics.Raycast(_bottom.position, transform.TransformDirection(direction), out RaycastHit bottomHit, _checkingDistance);
        bool isSomethingAtStepHeight = Physics.Raycast(_bottom.position + new Vector3(0, _maxStepHeight, 0), transform.TransformDirection(direction), out RaycastHit topHit, _checkingDistance);

        return isSomthingAtBottom && isSomethingAtStepHeight == false;
    }
}

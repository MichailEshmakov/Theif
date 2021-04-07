using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour
{
    private Mover _mover;

    private void Start()
    {
        _mover = GetComponent<Mover>();
    }

    void FixedUpdate()
    {
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector3 direction = Vector3.forward * verticalMove + horizontalMove * Vector3.right;

        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        _mover.Move(direction, direction.magnitude * _mover.MaxSpeed);
    }
}

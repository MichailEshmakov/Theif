using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PlayerController : MonoBehaviour
{
    private Mover _person;

    private void Start()
    {
        _person = GetComponent<Mover>();
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

        _person.Move(direction, direction.magnitude * _person.MaxSpeed);
    }
}

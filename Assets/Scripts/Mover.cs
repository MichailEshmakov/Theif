using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxSpeed;

    private Rigidbody _rigidbody;
    private float _speed;

    public float MaxSpeed => _maxSpeed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (value > MaxSpeed)
            {
                _speed = MaxSpeed;
            }
            else
            {
                _speed = value;
            }
        }
    }

    private void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();

    }

    public void Move(Vector3 direction, float speed)
    {
        Speed = speed;
        Quaternion neededRotation = direction == Vector3.zero ? transform.rotation : Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, _rotationSpeed * Time.deltaTime);
        direction.Normalize();
        _rigidbody.velocity = new Vector3(direction.x * speed, _rigidbody.velocity.y, direction.z * speed);
    }
}

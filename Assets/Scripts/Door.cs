using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _openingSpeed;
    [SerializeField] private float _keepingOpenTime;
    [SerializeField] private Vector3 _openingRotationDirection;

    private bool _isOpen;
    private Quaternion _startRotation;

    private void Start()
    {
        _isOpen = false;
        _startRotation = transform.rotation;
        _openingRotationDirection.Normalize();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Mover opener) && _isOpen == false)
        {
            float angleToOpener = Quaternion.FromToRotation(opener.transform.position - transform.position, transform.right).eulerAngles.y;
            StartCoroutine(Open(NormalizeAngle(angleToOpener) * 90));
        }
    }

    private IEnumerator Open(int degrees)
    {
        _isOpen = true;
        Quaternion neededRotation = Quaternion.Euler(_openingRotationDirection * degrees);

        while (transform.rotation != neededRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, _openingSpeed);
            yield return null;
        }

        StartCoroutine(WaitClosing());
    }

    private IEnumerator WaitClosing()
    {
        yield return new WaitForSeconds(_keepingOpenTime);
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        _isOpen = false;

        while (transform.rotation != _startRotation && _isOpen == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _startRotation, _openingSpeed);
            yield return null;
        }
    }

    private int NormalizeAngle(float degrees)
    {
        degrees %= 360;
        if (degrees == 0)
        {
            return 0;
        }
        else
        {
            if ((degrees < 0 && degrees > -180) || degrees > 180)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}

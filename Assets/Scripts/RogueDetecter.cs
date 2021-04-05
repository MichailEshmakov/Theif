using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RogueDetecter : MonoBehaviour
{
    [SerializeField] UnityEvent _onFirstRogueEnter;
    [SerializeField] UnityEvent _onAllRougesExit;

    private int _roguesInsideAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Mover rogue))
        {
            if (_roguesInsideAmount == 0)
            {
                _onFirstRogueEnter?.Invoke();
            }

            _roguesInsideAmount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Mover rogue))
        {
            _roguesInsideAmount--;
            if (_roguesInsideAmount == 0)
            {
                _onAllRougesExit?.Invoke();
            }
        }
    }
}

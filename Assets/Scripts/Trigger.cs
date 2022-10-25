using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    public event Action<Collider> Enter;

    private void OnTriggerEnter(Collider other)
    {
        Enter?.Invoke(other);
    }
}

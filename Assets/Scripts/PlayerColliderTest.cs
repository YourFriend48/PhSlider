using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy player))
        {
            Debug.Log("enter");
        }
    }
}

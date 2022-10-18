using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Finance;

public class Gem : MonoBehaviour
{
    [SerializeField, Min(1)] private int _value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Collect();
        }
    }

    private void Collect()
    {
        WalletHolder.Instance.PutIn(_value);

        //Ёффект сбора
        //јнимировать исчезновение
        //”ничтожить объект
    }
}

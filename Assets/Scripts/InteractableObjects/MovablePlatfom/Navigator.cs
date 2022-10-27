using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private NavigationPlatform[] _platforms;
    [SerializeField] private NavigationType _type;

    private void Init()
    {

    }


    enum NavigationType
    {
        Circle
    }
}

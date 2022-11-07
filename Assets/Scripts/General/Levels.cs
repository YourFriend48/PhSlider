using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
    public class Levels : ScriptableObject
    {
        [SerializeField] private string[] _names;

        public IReadOnlyList<string> Names => _names;
    }
}
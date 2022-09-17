using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    [SerializeField] private int _inital;

    private int _current;

    public event UnityAction<int, int> Changed;

    public int Current
    {
        get => _current;
        private set
        {
            _current = value;
            Changed?.Invoke(_inital, _current);
        }
    }

    public void Add(int power)
    {
        Current += power;
    }

    private void Start()
    {
        Current = _inital;
    }
}
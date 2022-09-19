using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    [SerializeField] private int _inital;

    private int _current;

    public event UnityAction<int> Changed;

    public int Current
    {
        get => _current;
        private set
        {
            _current = value;
            Changed?.Invoke(_current);
        }
    }

    public void Increase()
    {
        Current++;
    }

    private void Awake()
    {
        Current = _inital;
    }
}
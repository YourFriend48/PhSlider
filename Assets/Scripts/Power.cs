using TMPro;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField] private int _inital;
    [SerializeField] private TMP_Text _displayText;

    private int _current;

    public int Current
    {
        get => _current;
        private set
        {
            _current = value;
            _displayText.text = _current.ToString();
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
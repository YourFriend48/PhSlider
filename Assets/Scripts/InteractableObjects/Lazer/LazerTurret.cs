using UnityEngine;

public class LazerTurret : MonoBehaviour
{
    [SerializeField] private int _length;
    [SerializeField] private Lazer _lazer;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Vector3 middlePoint = _lazer.transform.position + (_length - 1) / 2f * _lazer.transform.forward;

        _lazer.transform.position = middlePoint;
        _lazer.transform.localScale = new Vector3(1, 1, _length);
        _lazer.Init();
    }
}

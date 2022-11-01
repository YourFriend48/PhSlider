using UnityEngine;

public class PlatformAnimator<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Vector3 _translation = new Vector3(0, 0.5f, 0);
    [SerializeField] private IoIoMover _cube;
    [SerializeField] private IoIoMover _colorablePart;

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
    }

    private void OnEnter(Collider other)
    {
        if (other.GetComponent<T>())
        {
            _trigger.gameObject.SetActive(false);
            _cube.Move(_cube.transform.localPosition + _translation);
            _colorablePart.Move(_colorablePart.transform.localPosition + _translation);
        }
    }
}

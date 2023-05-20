using UnityEngine;

public class TutorialCross : MonoBehaviour
{
    [SerializeField] private IoIoScaler _IoIoScaler;
    [SerializeField] private Vector3 _endScale = Vector3.one * 1.1f;

    public void Enable()
    {
        _IoIoScaler.Completed += OnCicleCompleted;
        _IoIoScaler.Scale(_endScale);
    }

    private void OnDisable()
    {
        _IoIoScaler.Completed -= OnCicleCompleted;
    }

    private void OnCicleCompleted()
    {
        _IoIoScaler.Scale(_endScale);
    }
}

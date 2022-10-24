using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class EndScreen : MonoBehaviour
{
    [SerializeField] protected Button Button;

    private CanvasGroup _canvasGroup;
    public event UnityAction Closed;

    public event UnityAction Opened;

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        Closed?.Invoke();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        Opened?.Invoke();
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
        Enable();
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
        Disable();
    }

    protected abstract void OnButtonClick();

    protected abstract void Disable();

    protected abstract void Enable();
}
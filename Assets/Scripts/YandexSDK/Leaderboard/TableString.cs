using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RectMover))]
public class TableString : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private float _speed;

    private RectTransform _rectTransform;
    private RectMover _rectMover;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectMover = GetComponent<RectMover>();
    }

    public void SetRectPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
    }

    public void SetName(string text)
    {
        _name.text = text;
    }

    public void SetScore(string text)
    {
        _score.text = text;
    }

    public void MoveTo(Vector2 target, float requireTime)
    {
        _rectMover.MoveTo(target, requireTime);
    }
}

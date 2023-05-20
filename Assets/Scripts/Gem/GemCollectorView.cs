using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectorView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _gemPanel;
    [SerializeField] private GameObject _UICristals;

    private Camera _camera;
    private Vector2 _gemPanelScreenPosition;

    private void Awake()
    {
        _camera = Camera.main;
        _gemPanelScreenPosition = _camera.WorldToScreenPoint(_gemPanel.position);
    }

    private Vector2 GetCanvasProjectionPoint(Vector3 position)
    {
        //Верно ли это? возвращается Vector3
        Vector2 screenPosition = _camera.WorldToScreenPoint(position);
        float ratioX = screenPosition.x / Screen.width;
        float ratioY = screenPosition.y / Screen.height;
        Vector2 canvasPoint = new Vector2(_canvas.pixelRect.width * ratioX, _canvas.pixelRect.height * ratioY);
        return canvasPoint;
    }

    private void Create(Vector3 position)
    {
        Instantiate(_UICristals, _canvas.transform.position, Quaternion.identity, _canvas.transform);
        RectTransform rectTransform = new RectTransform();
        rectTransform.anchoredPosition = GetCanvasProjectionPoint(position);
        rectTransform.parent = _UICristals.transform;
    }


}

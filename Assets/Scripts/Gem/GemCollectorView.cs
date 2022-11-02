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

    private Vector3 GetCanvasProjectionPoint(Vector3 position)
    {
        //Верно ли это? возвращается Vector3
        Vector2 screenPosition = _camera.WorldToScreenPoint(position);
        float ratioX = screenPosition.x / Screen.width;
        float ratioY = screenPosition.y / Screen.height;
        //Vector2 canvasPoint = _canvas.pixelRect.width


        //Vector2 delta = screenPosition - _gemPanelScreenPosition;
        //Vector3 = new Vector3(delta.x, delta.y, delta.z);
        //_gemPanel.position + delta;
        return Vector3.zero;
    }

    //private void Instant()
    //{
    //    Instantiate(_UICristals, , _gemPanel);
    //}


}

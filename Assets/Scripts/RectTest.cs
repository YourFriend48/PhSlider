using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTest : MonoBehaviour
{
    [SerializeField] private Image _template;

    private void Start()
    {
        Image image = Instantiate(_template, transform);
        image.rectTransform.anchoredPosition = Vector2.zero;
    }
}

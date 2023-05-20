using UnityEngine;
using TMPro;
using System.Collections;

namespace Finance
{
    [RequireComponent(typeof(TMP_Text))]
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private Price _price;
        [SerializeField] private float _stepsCount;

        private TMP_Text _text;
        private int _value;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _value = _price.Value;
            _text.text = _value.ToString();
            _price.PriceChanged += OnPriceChanged;
            _price.PriceSetted += OnPriceSetted;
        }

        private void OnDisable()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _price.PriceChanged -= OnPriceChanged;
            _price.PriceSetted -= OnPriceSetted;
        }

        private void OnPriceSetted(int value)
        {
            _value = value;
            _text.text = _value.ToString();
        }

        private void OnPriceChanged(int target)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            if (target != 0)
            {
                _currentCoroutine = StartCoroutine(Change(target));
            }
        }

        private IEnumerator Change(int target)
        {
            int distance = Mathf.Abs(target - _value);
            int delta = (int)(distance / _stepsCount);
            delta = Mathf.Clamp(delta, 1, distance);

            while (_value != target)
            {
                _value = MyMathf.MoveTowards(_value, target, delta);
                _text.text = _value.ToString();
                yield return null;
            }
        }
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

namespace Finance
{
    [RequireComponent(typeof(TMP_Text))]
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private float _stepsCount;

        private TMP_Text _text;
        private int _value;
        private Coroutine _valueChangingCoroutine;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Enable()
        {
            _value = WalletHolder.Instance.Value;
            _text.text = _value.ToString();
            WalletHolder.Instance.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            WalletHolder.Instance.BalanceChanged -= OnBalanceChanged;
        }

        private void SetValue(int value)
        {
            _value = value;
            _text.text = _value.ToString();
        }

        private void OnBalanceChanged(int target)
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            _valueChangingCoroutine = StartCoroutine(Change(target));
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

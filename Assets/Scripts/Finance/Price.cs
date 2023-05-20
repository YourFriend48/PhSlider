using UnityEngine;
using UnityEngine.Events;

namespace Finance
{
    public class Price : MonoBehaviour
    {
        [SerializeField] private int _startValue;
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _type = "Buy object";

        private int _value;

        public event UnityAction<int> PriceChanged;
        public event UnityAction<int> PriceSetted;
        public event UnityAction<string, string, int> Purchased;

        public int Value => _value;
        public int StartValue => _startValue;
        public string Name => _name;
        public string Type => _type;

        private void OnEnable()
        {
            PriceChanged += Save;
        }

        private void OnDisable()
        {
            PriceChanged -= Save;
        }

        public void Init()
        {
            if (PlayerPrefs.HasKey(_id))
            {
                _value = PlayerPrefs.GetInt(_id, _value);
            }
            else
            {
                _value = _startValue;
                PlayerPrefs.SetInt(_id, _value);
            }

            PriceChanged?.Invoke(_value);
        }

        public void Decrease(int value)
        {
            _value -= value;

            if (_value < 0)
            {
                _value = 0;
            }

            PriceChanged?.Invoke(_value);
        }

        public void SetId(string id)
        {
            _id = id;
        }

        public void SetStartValue(int value)
        {
            _startValue = value;
            PriceSetted?.Invoke(value);
        }

        public void SetPrice(int value)
        {
            _value = value;
            PriceChanged?.Invoke(_value);
        }

        private void Save(int value)
        {
            PlayerPrefs.SetInt(_id, value);

            if (value == 0)
            {
                Purchased?.Invoke(_type, _name, _startValue);
            }
        }
    }
}
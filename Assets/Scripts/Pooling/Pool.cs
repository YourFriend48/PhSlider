using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private int _length;
        [SerializeField] private Poolable _prefab;

        private List<Poolable> _items;
        private Stack<Poolable> _inactiveItems;

        public Type Type => _prefab.GetType();

        private void OnDisable()
        {
            foreach (Poolable item in _items)
            {
                item.Deactivated -= Push;
            }
        }

        public void Init()
        {
            Initialize();
        }

        public Poolable GetItem()
        {
            if (_inactiveItems.Count == 0)
            {
                Create();
            }

            Poolable item = _inactiveItems.Pop();
            item.transform.rotation = _prefab.transform.rotation;
            return item;
        }

        private void Create()
        {
            Poolable item = Instantiate(_prefab, transform);

            if (item.gameObject.activeSelf)
            {
                throw new Exception("Prefab must be deactivate");
            }

            item.Deactivated += Push;
            _items.Add(item);
            _inactiveItems.Push(item);
        }

        private void Push(Poolable item)
        {
            _inactiveItems.Push(item);
        }

        private void Initialize()
        {
            _items = new List<Poolable>(_length);
            _inactiveItems = new Stack<Poolable>(_length);

            for (int i = 0; i < _length; i++)
            {
                Create();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class PoolManager : MonoBehaviour
    {
        private Dictionary<Type, PoolBase> _pools;

        public static PoolManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            _pools = new Dictionary<Type, PoolBase>();
        }

        public void Init()
        {
            Pool[] pools = GetComponentsInChildren<Pool>();

            foreach (Pool pool in pools)
            {
                pool.Init();
                Type type = pool.Type;

                if (_pools.ContainsKey(type) == false)
                {
                    PoolBase poolBase = new PoolBase();
                    _pools.Add(type, poolBase);
                }

                _pools[type].Add(pool);
            }
        }

        public T Take<T>() where T : Poolable
        {
            Type type = typeof(T);

            if (_pools.ContainsKey(type))
            {
                Pool pool = _pools[type].GetRandomPool();
                return (T)pool.GetItem();
            }

            throw new ArgumentException("Pool manager does not contain this type of object: " + nameof(T));
        }
    }
}
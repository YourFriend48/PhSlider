using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class PoolBase
    {
        private List<Pool> _pools;

        public PoolBase()
        {
            _pools = new List<Pool>();
        }

        public void Add(Pool pool)
        {
            _pools.Add(pool);
        }

        public Pool GetRandomPool()
        {
            int index = Random.Range(0, _pools.Count);
            return _pools[index];
        }
    }
}
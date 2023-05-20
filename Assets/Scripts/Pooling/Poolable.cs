using UnityEngine;
using UnityEngine.Events;

namespace Pooling
{
    public abstract class Poolable : MonoBehaviour
    {
        public event UnityAction<Poolable> Deactivated;

        private void OnDisable()
        {
            Disable();
            Deactivated?.Invoke(this);
        }

        protected abstract void Disable();
    }
}
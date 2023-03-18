using System;

namespace Fury
{
    public class NetworkController
    {
        public event Action OnChanged;
        bool _dirty;

        public void SetDirty()
        {
            _dirty = true;
        }

        public void Update()
        {
            if (_dirty)
            {
                _dirty = false;
                OnChanged?.Invoke();
            }
        }

        public NetworkUser NewUser(string id)
        {
            SetDirty();
            return new NetworkUser(this, id);
        }
    }
}

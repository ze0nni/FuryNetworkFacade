using UnityEngine;

namespace Fury
{
    public class NetworkFacadeComponent : MonoBehaviour
    {
        internal NetworkController Controller;

        private void Update()
        {
            Controller?.Update();
        }
    }
}

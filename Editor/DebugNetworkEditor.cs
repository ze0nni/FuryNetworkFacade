using UnityEditor;
using UnityEngine;

namespace Fury.DebugNetwork
{
    [CustomEditor(typeof(DebugNetworkPrefs))]
    public class DebugNetworkEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUI.changed)
            {
                DebugNetworkPrefs.MarkDirty();
            }
        }
    }
}

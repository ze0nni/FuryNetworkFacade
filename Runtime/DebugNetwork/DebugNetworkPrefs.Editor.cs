using System.IO;
using UnityEngine;
namespace Fury.DebugNetwork
{
    public partial class DebugNetworkPrefs
    {
        private static string AssetName = "DebugNetworkPrefs";
        private static string EditorPath = $"Assets/Resources/{AssetName}.asset";

        public static DebugNetworkPrefs Instance
        {
            get
            {
#if UNITY_EDITOR
                var prefs = UnityEditor.AssetDatabase.LoadAssetAtPath<DebugNetworkPrefs>(EditorPath);
                if (prefs == null)
                {
                    prefs = ScriptableObject.CreateInstance<DebugNetworkPrefs>();
                    Directory.CreateDirectory(Path.GetDirectoryName(EditorPath));
                    UnityEditor.AssetDatabase.CreateAsset(prefs, EditorPath);
                }
                return prefs;
#else
                return Resources.Load<DebugNetworkPrefs>(AssetName)
                    ?? ScriptableObject.CreateInstance<DebugNetworkPrefs>();
#endif
            }
        }

        public static void MarkDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(Instance);
#endif
        }
    }
}
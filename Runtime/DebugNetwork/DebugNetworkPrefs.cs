using UnityEngine;
namespace Fury.DebugNetwork
{
    public partial class DebugNetworkPrefs : ScriptableObject
    {
        [Header("User")]
        public string UserId = "debug0";
        public string UserNickname = "Debug user";
        public Sprite UserPhoto;

        [Header("Ads")]
        public bool ShowFullscreenVideoSuccess = true;
        public bool HasRewardVideo = true;
        public bool RewardVideoSuccessed = true;
    }
}
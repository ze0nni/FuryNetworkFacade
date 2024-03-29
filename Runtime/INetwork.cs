using System.Threading.Tasks;

namespace Fury
{
    public interface INetwork
    {
        Task<NetworkUser> Init(NetworkController controller);

        Task<string> LoadData(string key);
        void SaveData(string key, string data);
        void DeleteData(string key);

        Task<bool> ShowFullscreenVideo();

        bool HasRewardVideo { get; }
        Task<bool> ShowRewardVideo();
    }
}
using System.Threading.Tasks;

namespace Fury
{
    public interface INetwork
    {
        Task Init();

        Task<string> LoadData(string key);
        void SaveData(string key, string data);
        void DeleteData(string key);
    }
}
using System.Threading.Tasks;

namespace Fury
{
    public interface INetwork
    {
        Task Init();
        IStorage<T> Storage<T>(string key) where T : class;
    }

    public interface IStorage<T> where T : class
    {
        Task<T> Load();
        void Save(T data);
        void Delete();
    }
}
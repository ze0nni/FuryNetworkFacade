using System.Threading.Tasks;

namespace Fury.DebugNetwork
{
    public class DebugNetwork : INetwork
    {
        public Task Init()
        {
            return Task.CompletedTask;
        }

        public IStorage<T> Storage<T>(string key) where T : class
        {
            return new Storage<T>(key);
        }
    }
}

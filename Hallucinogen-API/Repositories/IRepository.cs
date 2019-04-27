using System;
using System.Threading.Tasks;

namespace Hallucinogen_API.Repositories
{
    public interface IRepository : IDisposable
    {
        Task<bool> SaveAsync();
    }
}
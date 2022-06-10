using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        
        Task<bool> Delete(int id);

           

    }
}

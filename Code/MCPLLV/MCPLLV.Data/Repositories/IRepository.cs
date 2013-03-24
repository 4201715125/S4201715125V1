using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Repositories
{    
    public interface IRepository<T>
    {
        T GetById(int id);
        void Add(T entity);
        void Remove(T entity);
    }
}

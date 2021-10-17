using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Interfaces
{
    public interface IRepository<T>
    {
        Task<int> Add(T objeto);
        Task<int> Delete(int id);
        Task<int> Update(T objeto);
        Task<T> GetOne(int id);
        Task<IEnumerable<T>> GetAll();
       

    }
}

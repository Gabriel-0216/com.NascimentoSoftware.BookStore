using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.UnityOfWork
{
    public interface IUnityOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void RollBack();
    }
}

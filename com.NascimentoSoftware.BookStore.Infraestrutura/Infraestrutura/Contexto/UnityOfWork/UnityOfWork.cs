using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.UnityOfWork
{
    public sealed class UnityOfWork : IUnityOfWork
    {
        private readonly DbSession _dbSession;

        public UnityOfWork(DbSession dbSession)
        {
            _dbSession = dbSession;
        }
        public void BeginTransaction()
        {
            _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbSession.Transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            _dbSession.Transaction.Dispose();
        }

        public void RollBack()
        {
            _dbSession.Transaction.Rollback();
            Dispose();
        }
    }
}

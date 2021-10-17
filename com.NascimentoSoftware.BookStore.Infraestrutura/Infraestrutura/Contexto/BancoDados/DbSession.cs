using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public DbSession()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}

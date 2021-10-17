using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository
{
    public class AutorRepository : IRepository<Autor>
    {
        private DbSession _dbSession;

        public AutorRepository(DbSession session)
        {
            _dbSession = session;
        }
        public async Task<int> Add(Autor objeto)
        {
            var param = new DynamicParameters();
            param.Add("Nome", objeto.Nome);
            param.Add("Sobrenome", objeto.Sobrenome);
            param.Add("DataRegistro", objeto.DataRegistro);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);

            var query = $@"INSERT INTO Autor (Nome, Sobrenome, DataRegistro, DataAtualizacao)
                        VALUES(@Nome, @Sobrenome, @DataRegistro, @DataAtualizacao)";
            return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);


        }

        public async Task<int> Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);

            var query = $@"DELETE FROM Autor WHERE Id = @Id";

            return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
        }

        public async Task<IEnumerable<Autor>> GetAll()
        {
            try
            {
                var query = @$"SELECT Id, Nome, Sobrenome, DataRegistro, DataAtualizacao FROM Autor";
                return await _dbSession.Connection.QueryAsync<Autor>(query, transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Autor> GetOne(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);

            var query = $@"SELECT Id, Nome, Sobrenome, DataRegistro, DataAtualizacao
                            FROM Autor WHERE Id = @Id";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Autor>(query, param: param, transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);

        }

        public async Task<int> Update(Autor objeto)
        {
            var param = new DynamicParameters();
            param.Add("Id", objeto.Id);
            param.Add("Nome", objeto.Nome);
            param.Add("Sobrenome", objeto.Sobrenome);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);

            var query = $@"UPDATE Autor SET Nome = @Nome, Sobrenome = @Sobrenome, 
                        DataAtualizacao = @DataAtualizacao WHERE Id = @Id";

            return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
        }
    }
}

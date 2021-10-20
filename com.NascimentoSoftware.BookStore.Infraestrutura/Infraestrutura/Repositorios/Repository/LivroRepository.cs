using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository
{
    public class LivroRepository : IRepository<Livro>
    {
        private readonly DbSession _dbSession;
        public LivroRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }
        public async Task<int> Add(Livro objeto)
        {
            var param = new DynamicParameters();
            param.Add("Nome", objeto.Nome);
            param.Add("DataRegistro", objeto.DataRegistro);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);
            param.Add("CategoriaId", objeto.CategoriaId);
            try
            {
                var query = $@"INSERT INTO Livro (Nome, DataRegistro, DataAtualizacao, CategoriaId)
                               OUTPUT Inserted.Id VALUES (@Nome, @DataRegistro, @DataAtualizacao, @CategoriaId)";

                return await _dbSession.Connection.ExecuteScalarAsync<int>(query, param: param, 
                    transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetId(Livro livro)
        {
            var param = new DynamicParameters();
            param.Add("Nome", livro.Nome);
            param.Add("DataRegistro", livro.DataRegistro);
            param.Add("DataAtualizacao", livro.DataAtualizacao);
            param.Add("CategoriaId", livro.CategoriaId);
            try
            {
                var query = $@"SELECT Id FROM Livro WHERE Nome = @Nome AND DataRegistro = @DataRegistro AND DataAtualizacao = @DataAtualizacao AND CategoriaId = @CategoriaId";
                return await _dbSession.Connection.ExecuteScalarAsync<int>(query, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return 0;
            }
        }



        public async Task<int> Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);

            try
            {
                var query = $@"DELETE FROM Livro WHERE ID = @Id";
                return await _dbSession.Connection.ExecuteAsync(query, param: param,
                    transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Livro>> GetAll()
        {
            try
            {
                var query = $@"SELECT Id, Nome, DataRegistro, DataAtualizacao, CategoriaId from LIVRO";
                return await _dbSession.Connection.QueryAsync<Livro>(query, transaction: _dbSession.Transaction,
                    commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Livro> GetOne(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            try
            {
                var query = $@"SELECT Id, Nome, DataRegistro, DataAtualizacao, CategoriaId from LIVRO WHERE Id = @Id";
                return await _dbSession.Connection.QueryFirstOrDefaultAsync<Livro>(query, param: param, transaction: _dbSession.Transaction,
                    commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Update(Livro objeto)
        {
            var param = new DynamicParameters();
            param.Add("Id", objeto.Id);
            param.Add("Nome", objeto.Nome);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);
            try
            {
                var query = $@"UPDATE Livro SET Nome = @Nome, DataAtualizacao = @DataAtualizacao WHERE Id = @Id";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, 
                    commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

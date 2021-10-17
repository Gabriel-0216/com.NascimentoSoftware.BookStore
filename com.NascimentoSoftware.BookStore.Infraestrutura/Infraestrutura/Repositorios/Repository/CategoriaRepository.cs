using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        private DbSession _dbSession;

        public CategoriaRepository(DbSession session)
        {
            _dbSession = session;
        }

        public async Task<int> Add(Categoria objeto)
        {
            var param = new DynamicParameters();
            param.Add("Nome", objeto.Nome);
            param.Add("DataRegistro", objeto.DataRegistro);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);

            try
            {
                var query = $@"INSERT INTO Categoria (Nome, DataRegistro, DataAtualizacao)
                            VALUES (@Nome, @DataRegistro, @DataAtualizacao)";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction,
                    commandType: System.Data.CommandType.Text);

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
                var query = $@"DELETE FROM Categoria WHERE Id = @Id";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction,
                    commandType: System.Data.CommandType.Text);
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            try
            {
                var query = $@"SELECT Id, Nome, DataRegistro, DataAtualizacao FROM CATEGORIA";
                return await _dbSession.Connection.QueryAsync<Categoria>(query, transaction: _dbSession.Transaction,
                                                                commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Categoria> GetOne(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            try
            {
                var query = $@"Select Id, Nome, DataRegistro, DataAtualizacao FROM Categoria
                                                                            where Id = @Id";
                return await _dbSession.Connection.QueryFirstOrDefaultAsync<Categoria>(query, param: param,
                            transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return null; ;
            }
        }

        public async Task<int> Update(Categoria objeto)
        {
            var param = new DynamicParameters();
            param.Add("Id", objeto.Id);
            param.Add("Nome", objeto.Nome);
            param.Add("DataAtualizacao", objeto.DataAtualizacao);
            try
            {
                var query = $@"UPDATE Categoria SET Nome = @Nome, DataAtualizacao = @DataAtualizacao where Id = @Id";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, transaction: _dbSession.Transaction,
                                                                        commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

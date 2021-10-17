using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Processos;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository
{
    public class LivroAutorRepository : IRepository<LivroAutor>
    {
        private readonly DbSession _dbSession;
        public LivroAutorRepository(DbSession session)
        {
            _dbSession = session;
        }
        public async Task<int> Add(LivroAutor objeto)
        {
            var param = new DynamicParameters();
            param.Add("AutorId", objeto.AutorId);
            param.Add("LivroId", objeto.LivroId);
            try
            {
                var query = $@"INSERT INTO LivroAutor (AutorId, LivroId) VALUES (@AutorId, @LivroId)";
                return await _dbSession.Connection.ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public async Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Delete(int IdAutor, int IdLivro)
        {
            var param = new DynamicParameters();
            param.Add("IdAutor", IdAutor);
            param.Add("IdLivro", IdLivro);
            try
            {
                var query = $@"DELETE FROM LivroAutor WHERE AutorId = @IdAutor AND LivroId = @IdLivro";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<LivroAutor>> GetAll()
        {
            try
            {
                var query = $@"SELECT Id, AutorId, LivroId FROM LivroAutor";
                return await _dbSession.Connection.QueryAsync<LivroAutor>(query, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<LivroAutor> GetOne(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<LivroAutor> GetOne(int IdLivro, int IdAutor)
        {
            var param = new DynamicParameters();
            param.Add("IdAutor", IdAutor);
            param.Add("IdLivro", IdLivro);
            try
            {
                var query = $@"SELECT Id, AutorId, LivroId FROM LivroAutor WHERE AutorId = @IdAutor AND LivroId = @IdLivro";
                return await _dbSession.Connection.QueryFirstOrDefaultAsync<LivroAutor>(query, param: param, commandType: System.Data.CommandType.Text,
                    transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Update(LivroAutor objeto)
        {
            var param = new DynamicParameters();
            param.Add("Id", objeto.Id);
            param.Add("LivroId", objeto.LivroId);
            param.Add("AutorId", objeto.AutorId);
            try
            {
                var query = $@"UPDATE LivroAutor SET AutorId = @Id, LivroId = @LivroId WHERE Id = @Id";
                return await _dbSession.Connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);

            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

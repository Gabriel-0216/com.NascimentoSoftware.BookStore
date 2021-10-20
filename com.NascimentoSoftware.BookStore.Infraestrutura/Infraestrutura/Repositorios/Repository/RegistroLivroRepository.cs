using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository
{
    public class RegistroLivroRepository
    {
        private readonly DbSession _dbSession;

        public RegistroLivroRepository(DbSession session)
        {
            _dbSession = session;
        }
        public async Task<bool> InserirLivro(Livro livro, Autor autor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Nome", livro.Nome);
                param.Add("DataRegistro", livro.DataRegistro);
                param.Add("DataAtualizacao", livro.DataAtualizacao);
                param.Add("CategoriaId", livro.CategoriaId);

                var query = $@"INSERT INTO Livro (Nome, DataRegistro, DataAtualizacao, CategoriaId)
                               OUTPUT Inserted.Id VALUES (@Nome, @DataRegistro, @DataAtualizacao, @CategoriaId)";

                int idLivroInserido = await _dbSession.Connection.ExecuteScalarAsync<int>(query, param: param,
                     transaction: _dbSession.Transaction, commandType: System.Data.CommandType.Text);

                var param_02 = new DynamicParameters();
                param_02.Add("AutorId", autor.Id);
                param_02.Add("LivroId", idLivroInserido);

                var query_02 = $@"INSERT INTO LivroAutor(AutorId, LivroId) VALUES (@AutorId, @LivroId)";

                await _dbSession.Connection.ExecuteAsync(query_02, param: param_02, commandType: System.Data.CommandType.Text,
                    transaction: _dbSession.Transaction);
                return true;

            }
            catch (Exception e)
            {
                _dbSession.Transaction.Rollback();
                return false;
            }

        }

        public async Task<bool> DeletarLivro(int idLivro)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("IdLivro", idLivro);

                var query = $@"DELETE FROM Livro WHERE Id = @IdLivro";
                await _dbSession.Connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);

                var query_02 = $@"DELETE FROM LivroAutor WHERE LivroId = @IdLivro";
                await _dbSession.Connection.ExecuteAsync(query_02, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);

                return true;
            }
            catch (Exception)
            {
                _dbSession.Transaction.Rollback();
                return false;
            }
        }
    }
}

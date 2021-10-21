using com.NascimentoSoftware.BookStore.Infraestrutura.Infra.E_commerce.Models;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Processos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infra.E_commerce.Processos
{
    public class AdicionarProdutoCarrinho
    {
        private readonly DbSession _dbSession;
        //quando o usuário clicar em ok -> inserir um registro numa tabela Carrinho_Produto
        //Necessário: Id (único) CarrinhoId, ProdutoId, ValorProduto
        public AdicionarProdutoCarrinho(DbSession session)
        {
            _dbSession = session;
        }
        //adc produto -> recebe id usuario, id produto e valorDoProduto
        public async Task<bool> AdicionarProduto(string Usuario, int ProdutoId, decimal ValorProduto)
        {
            try
            {
                var carrinho = await GetOrCreateCarrinho(Usuario);

                var param = new DynamicParameters();
                param.Add("CarrinhoId", carrinho.Id);
                param.Add("ProdutoId", ProdutoId);
                param.Add("ValorProduto", ValorProduto);

                var query = $@"INSERT INTO Produto_Carrinho(CarrinhoId, ProdutoId, ValorProduto)
                            values(@CarrinhoId, @ProdutoId, @ValorProduto)";

                await _dbSession.Connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text,
                    transaction: _dbSession.Transaction);
                return true;
            }
            catch (Exception)
            {
                _dbSession.Transaction.Rollback();
                return false;
            }
        }
        public async Task<bool> RemoverProduto(int CarrinhoId, int ProdutoId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("CarrinhoId", CarrinhoId);
                param.Add("ProdutoId", ProdutoId);

                var query = $@"DELETE FROM Produto_Carrinho WHERE CarrinhoId = @CarrinhoId and ProdutoId = @ProdutoId";
                await _dbSession.Connection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text, 
                    transaction: _dbSession.Transaction);
                return true;
            }
            catch (Exception)
            {
                _dbSession.Transaction.Rollback();
                return false;
            }
        }

        public async Task<Carrinho> GetOrCreateCarrinho(string Usuario)
        {
            try
            {
                var carrinho = await GetCarrinho(Usuario);
                if(carrinho == null)
                {
                    var param_02 = new DynamicParameters();
                    param_02.Add("Usuario", Usuario);
                    var query_02 = $@"INSERT INTO Carrinho(GuidUsuario) VALUES(@Usuario)";
                    await _dbSession.Connection.ExecuteAsync(query_02, param: param_02, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
                    carrinho = await GetCarrinho(Usuario);
                    return carrinho;
                }
                else
                {
                    return carrinho;
                }
            }
            catch(Exception)
            {
                return null;
            }

        }

        public async Task<Carrinho> GetCarrinho(string usuario)
        {
            var param = new DynamicParameters();
            param.Add("Usuario", usuario);
            var query = $@"SELECT Id, GuidUsuario from Carrinho WHERE GuidUsuario = @Usuario";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Carrinho>(query, param: param, commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
        }

        public async Task<IEnumerable<ProdutosCarrinho>> GetProdutosCarrinho(string usuario)
        {
            try
            {
                var Carrinho = await GetCarrinho(usuario);
                var param = new DynamicParameters();
                param.Add("CarrinhoId", Carrinho.Id);

                var query = $"SELECT Id, CarrinhoId, ProdutoId, ValorProduto from Produto_Carrinho where CarrinhoId = @CarrinhoId";

                return await _dbSession.Connection.QueryAsync<ProdutosCarrinho>(query, param: param, 
                            commandType: System.Data.CommandType.Text, transaction: _dbSession.Transaction);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

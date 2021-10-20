using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
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
        public async Task<bool> AdicionarProduto(int CarrinhoId, int ProdutoId, decimal ValorProduto)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("CarrinhoId", CarrinhoId);
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
    }
}

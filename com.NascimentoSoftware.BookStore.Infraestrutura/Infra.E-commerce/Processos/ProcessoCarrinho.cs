using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Contexto.BancoDados;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Processos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infra.E_commerce.Processos
{
    public class ProcessoCarrinho
    {
        public string GetConnection()
        {
            return Settings.ConnectionString;
        }
        //get carrinho - passando id usuario (carrinho é unico para o usuário)

        public async Task<Carrinho> GetCarrinhoAsync(string usuario)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("IdUsuario", usuario);

                var query = $@"SELECT Id, GuidUsuario from Carrinho WHERE GuidUsuario = @IdUsuario";
                using (var sql = new SqlConnection(GetConnection()))
                {
                    var carrinho = await sql.QueryFirstOrDefaultAsync<Carrinho>(query, param: param, commandType: System.Data.CommandType.Text);
                         
                if (carrinho != null)
                {
                    return carrinho;
                }
                else
                {
                    var param_02 = new DynamicParameters();
                    param_02.Add("GuidUsuario", usuario);
                    var query_criacao = $@"INSERT INTO Carrinho (GuidUsuario) VALUES (@GuidUsuario)";
                    await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text);

                    var param_03 = new DynamicParameters();
                    param_03.Add("GuidUsuario", usuario);
                    var query_retorno = $@"SELECT Id, GuidUsuario FROM CARRINHO WHERE GuidUsuario = @GuidUsuario";

                    return await sql.QueryFirstOrDefaultAsync<Carrinho>(query, param: param, commandType: System.Data.CommandType.Text);

                }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------





using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IClienteAcessoRepository: IGenericRepository<ClienteAcesso>
    {
        ClienteAcesso ValidateUser(string user, string senha);
        Task<List<PaginaAcesso>> PaginasAcessoListAsync();
        Task<List<PaginaAcesso>> PaginasAcessoListAsync(int idClienteAcesso);
        Task<List<ClienteAcessoLoja>> ClienteAcessoLojaListAsync(int idClienteAcesso);
        Task AdicionarClienteAcessoPerfilPaginas(int[] IdsPaginaAcesso, int IdClienteAcesso);
        Task RemoverClienteAcessoPerfilPaginas(int IdClienteAcesso);
        Task AdicionarClienteAcessoLoja(int IdClienteAcesso, int[] IdsLoja);
        Task RemoverClienteAcessoLoja(int IdClienteAcesso);
    }
}



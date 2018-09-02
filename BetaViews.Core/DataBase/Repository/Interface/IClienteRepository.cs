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
using BetaViews.Messages.SendReceiver.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IClienteRepository: IGenericRepository<Clientes>
    {
        Task<List<PalavraRecusadaPadrao>> PalavraRecusadaCliente(int idCliente);

        Task<ListarBadgeRS> ListarBadgeCliente(ListarBadgeRQ request);
        bool CheckTokenAuthorization(string token);


    }
}



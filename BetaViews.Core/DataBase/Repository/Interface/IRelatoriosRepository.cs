
using BetaViews.Messages.Models;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IRelatoriosRepository
    {

        RelMarcas RelMarcas(int idCliente, int IdLoja);

        RelDepartamentos RelDepartamentos(int idCliente, int IdLoja);

        RelatoriosDeLojas RelTopLojas(int? idCliente, int page);
    }
}

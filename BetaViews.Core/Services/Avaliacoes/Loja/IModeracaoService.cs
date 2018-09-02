using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Core.Services.Avaliacoes.Loja
{
    public interface IModeracaoService
    {
        Task<string> ExecutaModeracaoAvaliacoes();

        Task<string> ExecutaModeracaoQA();

    }
}

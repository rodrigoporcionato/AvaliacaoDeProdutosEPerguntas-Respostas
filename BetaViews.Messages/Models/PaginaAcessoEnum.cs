namespace BetaViews.Messages.Models
{
    public enum PaginaAcessoEnum
    {
        Principal_DASHBOARD = 1,
        Moderacao_ModeracaoProdutos = 2,
        Moderacao_ModeracaoLojasEMarketPlace = 3,
        Moderacao_PerguntasERespostas = 4,
        Avaliacao_AvaliacaoDeProdutos = 5,
        Avaliacao_AvaliacaoDeLojasEMarketPlace = 6,
        Relatorio_RelatoriosDeProdutos = 7,
        Relatorio_RelatoriosDeLojasEMarketPlace = 8,
        Relatorio_Categorias = 9,
        Relatorio_Marcas = 10,
        Relatorio_Clientes = 11,
        Configuracoes_ConfiguracoesGerais = 12,
        Configuracoes_ConfiguracoesDeClientes = 13,
        Configuracoes_ConfiguracaoDeLoja = 14,
        Configuracoes_ConfiguracaoDeAPI = 15,
        Cadastro_CadastroDeUsuarios = 16,
        Cadastro_PermissoesDeAcesso = 17,
        LogsSistema_LogsDoSistema = 18,

        //especico statico para qualquer outra pagina
        AcessoAutorizado = 5000
    }
}
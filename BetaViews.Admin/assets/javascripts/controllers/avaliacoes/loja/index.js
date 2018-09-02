$(document).ready(function () {

    //3 rejeitado 
    //5 aprovado.    

    $(document).on("click", ".btnRemoverAvaliacao", function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $('#aprovOuRejPanel_' + id).hide('fast');
        $('#aprovOuRejAlert_' + id).show('fast');
        RemoverAvaliacao(id, 4);
    });

    function RemoverAvaliacao(id, idstatus) {
        var data = {
            IdAvaliacao: id,
            idStatus: idstatus
        }
        $.post("/Avaliacao_AvaliacaoDeLojasEMarketPlace/RemoverAvaliacao", data, function (response) {
            if (response) {
                if (response.erro) {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Houve um erro nos nossos servidores',
                        type: 'danger'
                    });
                }
                else {

                    if (idstatus === 4) {
                        new PNotify({
                            title: 'Sucesso',
                            text: 'Avaliações removida com sucesso!',
                            type: 'success'
                        });
                    }
                    window.setTimeout(function () {
                        $('.tr_' + id).hide('fast');
                        $('.tr_' + id).remove();
                    }, 2000);

                }
            }
        });


    }




});
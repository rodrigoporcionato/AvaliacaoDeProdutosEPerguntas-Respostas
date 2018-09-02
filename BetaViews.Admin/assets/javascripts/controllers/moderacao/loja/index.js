$(document).ready(function () {

    //3 rejeitado 
    //5 aprovado.    
    $(document).on("click", ".btnAprovOuRejRadio", function (e) {
        var id = $(this).data('id');
        var opcaoSel = $('input[name=aprRej_' + id + ']:checked').val();

        if (opcaoSel == 3) {
            $('#OpcaoRejeicao_' + id).show('fast');
        } else {
            $('#OpcaoRejeicao_' + id).hide('fast');
        }

    });

    $(document).on("click", ".btnAprovOuRej", function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var opcaoSel = parseInt($('input[name=aprRej_' + id + ']:checked').val());

        if ($('input[name=aprRej_' + id + ']:checked').val() == undefined) {
            $('#aprovOuRejAlertError_' + id).show('fast');
            return;
        } else {
            $('#aprovOuRejAlertError_' + id).hide('fast');
            $('#aprovOuRejPanel_' + id).hide('fast');
            $('#aprovOuRejAlert_' + id).show('fast');
            $('#checkItem_' + id).prop('checked', true);
            EnviarAprovacaoOuReprovacao(e, opcaoSel);
            $('#checkItem_' + id).prop('checked', false);
        }
    });



    $("#ckbCheckAll").click(function () {
        $(".checkItem").prop('checked', $(this).prop('checked'));
    });


    $(document).on("click", ".aprovarTodosMarcados", function (e) {
        e.preventDefault();

        EnviarAprovacaoOuReprovacao(e, 5);
    });


    $(document).on("click", ".reprovarTodosMarcados", function (e) {
        e.preventDefault();

        EnviarAprovacaoOuReprovacao(e, 4);
    });


    function EnviarAprovacaoOuReprovacao(items, idstatus) {

        var selected_value = [];

        $(".checkItem:checkbox:checked").each(function () {
            selected_value.push($(this).val());
        });



        if (selected_value.length > 0) {

            var data = {
                avaliacoes: selected_value,
                idstatus: idstatus
            }

            $.post("/Moderacao_ModeracaoLojasEMarketPlace/AprovarAvaliacoes", data, function (response) {
                if (response) {
                    if (response.erro) {
                        new PNotify({
                            title: 'Erro!',
                            text: 'Houve um erro nos nossos servidores',
                            type: 'danger'
                        });
                    }
                    else {

                        if (idstatus === 5)
                            new PNotify({
                                title: 'Sucesso',
                                text: 'Avaliações aprovada(s) com sucesso!',
                                type: 'success'
                            });
                        else
                            new PNotify({
                                title: 'Sucesso',
                                text: 'Avaliações reprovada(s) com sucesso!',
                                type: 'success'
                            });

                        if (data.avaliacoes.length > 5) {
                            window.location = '/Moderacao_ModeracaoLojasEMarketPlace';
                        }
                        window.setTimeout(function () {
                            for (var i = 0; i < data.avaliacoes.length; i++) {
                                $('.tr_' + data.avaliacoes[i]).hide('fast');
                                $('.tr_' + data.avaliacoes[i]).remove();
                            }

                        }, 2000);

                    }
                }
            });

        } else {
            toastr.warning('Selecione pelo menos uma avaliação para aprovar!', 'Atenção!!!');
        }


    }




});
$(document).ready(function () {

    $(document).on('click', '.checkPai', function () {
        var modulo = $(this).data('modulo');
        var toggleCheck = (this.checked) ? true : false;

        $("ul").find("[data-modulo='" + modulo + "']").prop('checked', toggleCheck);

    });

    $(document).on('click', '#UsuarioMaster', function () {

        var usuarioMasterSelecionadoToggle = $(this).prop('checked');

        //console.log(usuarioMasterSelecionadoToggle);
        if (usuarioMasterSelecionadoToggle) {
            $('#perfilAcessoPersonalizado').hide();
        } else {
            $('#perfilAcessoPersonalizado').show();
        }
        


    });

    $(document).on('click', '.modal-confirm', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
        var id = $(this).attr('data-id');        
        $.post("/Cadastro_CadastroDeUsuarios/delete?id=" + id, function (data, textStatus, jqXHR) {
            $('#linha_' + id).hide('slow');

            new PNotify({
                title: 'Sucesso!',
                text: 'A\u00E7\u00E3o executada com sucesso!',
                type: 'success'
            });

        });        
    });



    $('[data-toggle=senha]').confirmation({
        rootSelector: '[data-toggle=senha]',
        onConfirm: function () {

            var id = $(this).attr('email');
            if (id != undefined)
                $.post("/Cadastro_CadastroDeUsuarios/SendNewPassword?id=" + id, function (response) {
                if (response) {

                    if (response.erro) {                        
                        new PNotify({
                            title: 'ops...',
                            text: 'Houve um erro nos nossos servidores',
                            type: 'danger'
                        });
                    }
                    else {                        
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Foi enviado um e-mail para o usuário. Aguarde o envio do e-mail dentro de alguns minutos.',
                            type: 'success'
                        });
                    }
                }
            });


        },
        onCancel: function () {

        },
        btnOkLabel: "sim, enviar",
        btnCancelLabel: "Cancelar envio"
    });



    




});
var betaViews = betaViews || {
    configs:{
        apistatus: false,
        urlwidget: '//localhost:63987/Content/js/widget.js',
        url: "//localhost:49675",
        urlstatic: "//localhost:63987",
        urlcss: "//localhost:63987/content/css/BetaViewsCSS.min.css",
        elQA: 'HTMLQA',
        elAv: 'HTMLAvaliacoes',
        filtroAV: '',
        filtroQA: '',
        QATermo: '',
        bvparams: null,
        cssLoad:false
    },
    init: function (bv) {
        this.configs.bvparams = bv!=undefined?bv:null;
        console.log(this.configs);
        this.checkInfo(this.configs.bvparams[0]);
        this.loadCss();
        this.loadComponents();
    },
    loadCss:function() {
        var b=document.createElement("link");
        b.rel="stylesheet";
        b.type="text/css";
        b.href = this.configs.urlcss;
        this.appendToRoot(b)
        this.configs.cssLoad = true;
    },
    appendToRoot:function(a) {
        (document.getElementsByTagName("head")[0]||document.getElementsByTagName("body")[0]).appendChild(a)
    },
    checkInfo : function (c) {
        if (c.PRD_CODIGO === undefined || c.PRD_CODIGO === "") {
            console.warn('PUSH5STARS = > Você precisa informar os dados de inicializa\u00E7\u00E3o em Push5StarsInit. Informe o código do produto. PRD_CODIGO')
            return;
        }
        else if (c.LOJA_CODIGO === undefined || c.LOJA_CODIGO === "" || c.LOJA_CODIGO === 0) {
            console.warn('PUSH5STARS = > Você precisa informar os dados de inicializa\u00E7\u00E3o em Push5StarsInit. Informe o código da loja. LOJA_CODIGO')
            return;
        }
        else if (c.EL_AVALIACAO === undefined || c.EL_AVALIACAO === "" || c.EL_AVALIACAO === "#") {
            console.warn('PUSH5STARS = > Você precisa informar os dados de inicializa\u00E7\u00E3o em Push5StarsInit. Informe o EL_AVALIACAO no seu HTML.')
            return;
        }
        else if (
            (c.DISPLAY_AVALICAO === undefined && c.DISPLAY_QA === undefined)
            || (c.DISPLAY_AVALICAO === false && c.DISPLAY_QA === false)
            ) {
            console.warn('PUSH5STARS = > Não foi habilitado avaliação e nem QA no site. verifique se um dos parametros estão como "true" em DISPLAY_AVALICAO ou DISPLAY_QA.')
        }
        else {
            console.info('PUSHSTAR VERIFICAÇÃO: carregamento OK');
            this.configs.apistatus = true;
        }
    },
    loadComponents: function () {
        if (!this.configs.cssLoad) {
            this.loadCss();
        }        
        window.jQuery = 'loading';
        var $ = document.createElement('script');
        $.type = 'text/javascript';
        $.src = this.configs.urlwidget;
        
        $.onload = function () {
            betaViews.loadWidget();            
        };
        $.onerror = function () {
            delete jQuery;
            console.error('Betaviews -> Erro. Components não foram carregados.');
        };
        document.getElementsByTagName('head')[0].appendChild($);
    },
    loadWidget: function () {
        var p = betaViews.configs.bvparams[0];
        var configs = betaViews.configs;

        //modal avaliacao
        $(document).on('click', '.btnOpenAvaliacao', function (event) {
            event.preventDefault();
            var form = $("#formEnvioAvaliacaoAnalise");
            form.parsley().reset();
            form[0].reset();
            $('#enviarAvaliacao').prop("disabled", false).text('Enviar avalia\u00E7\u00E3o para revis\u00E3o');
            $('#alertaAvalComent').toggleClass('in', false);
            $('.modal-footer').toggleClass('hidden', false);
            $('.avaliacaomsgok').toggleClass('hidden', true);
            $('.enviarAvaliacaoErro').text("");
            $(form).toggleClass('hidden', false);
            $('#modalAvaliacao').modal();
        });
        //form de avaliacao
        $(document).on('click', '.enviarAvaliacao', function (event) {
            event.preventDefault();
            var form = $("#formEnvioAvaliacaoAnalise");
            var validate = form.parsley().validate();
            if (validate == false) {

                if (form.parsley()._focusedField[0].id === "star-5-2") {
                    $('.modal').animate({
                        scrollTop: $("#star-5-2").offset().top
                    }, 0);
                }
                $('.enviarAvaliacaoErro').addClass('text-danger').text('Um ou mais campos não foram preenchidos.');
            }
            if (!validate) return;

            $('.enviarAvaliacaoErro').text("");

            $(this).prop("disabled", true).text('aguarde, processando...');


            var data = { "Avaliacao": $(form).serializeObject() };
            var loja = {
                LojaCodigo: p.LOJA_CODIGO,
                LojaNome: p.LOJA_NOME,
                LojaMarketPlace: p.LOJA_MKTP
            }
            var produto = {
                PrdNome: p.PRD_NOME,
                PrdCodigo: p.PRD_CODIGO,
                PrdDepartamento: p.PRD_DEPTO,
                PrdCategoria: p.PRD_CATEGORIA,
                PrdSubCategoria: p.PRD_SUBCATEGORIA,
                PrdMarca: p.PRD_MARCA,
                PrdLink: window.location.href,
                PrdImagemProduto: ""
            }
            data.Loja = loja;
            data.Produto = produto;
            data.Authorization = p.APIKEY;

            console.log(JSON.stringify(data));

            $.ajax({
                type: "POST",
                url: configs.url + "/Avaliacoes/enviar/produto",
                data: data,
                dataType: "json",
                success: function (response) {
                    if (!response.Valido) {
                        alert('Houve um erro no envio. Por favor tente mais tarde.');
                        return;
                    }
                    $(form).toggleClass('hidden', true);
                    $('.modal-footer').toggleClass('hidden', true);
                    $('.avaliacaomsgok').toggleClass('hidden', false);
                },
                error: function (xhr) {
                    var jsonResponse = JSON.parse(xhr.responseText);
                    console.log(jsonResponse.message);
                    alert('Houve um erro no envio. Por favor tente mais tarde.');
                }
            });
        });
        //modal QA
        $(document).on('click', '.btnOpenQA', function (event) {
            event.preventDefault();
            var form = $("#formEnvioQAAnalise");
            form.parsley().reset();
            form[0].reset();
            $('#enviarQA').prop("disabled", false).text('Enviar pergunta para revis\u00E3o');
            $('#alertaQAComent').toggleClass('in', false);
            $('.modal-footer').toggleClass('hidden', false);
            $('.qamsgok').toggleClass('hidden', true);
            $('.enviarQAErro').text("");
            $(form).toggleClass('hidden', false);
            $('#modalQA').modal();
        });
        //form de QA
        $(document).on('click', '.enviarQA', function (event) {
            event.preventDefault();
            var form = $("#formEnvioQAAnalise");
            var validate = form.parsley().validate();
            if (validate == false) {
                $('.enviarQAErro').addClass('text-danger').text('Um ou mais campos não foram preenchidos.');
            }
            if (!validate) return;
            $('.enviarQAErro').text("");
            $(this).prop("disabled", true).text('aguarde, processando...');
            var loja = {
                LojaCodigo: p.LOJA_CODIGO,
                LojaNome: p.LOJA_NOME,
                LojaMarketPlace: p.LOJA_MKTP
            }
            var Produto = {
                PrdNome: p.PRD_NOME,
                PrdCodigo: p.PRD_CODIGO,
                PrdDepartamento: p.PRD_DEPTO,
                PrdCategoria: p.PRD_CATEGORIA,
                PrdSubCategoria: p.PRD_SUBCATEGORIA,
                PrdMarca: p.PRD_MARCA,
                PrdLink: window.location.href,
                PrdImagemProduto: ""
            }
            var data = $(form).serializeObject();
            data.Loja = loja;
            data.Produto = Produto;
            data.Authorization = p.APIKEY;
            $.ajax({
                type: "POST",
                url: configs.url + "/PerguntasERespostas/EnviarDuvida",
                data: data,
                dataType: "json",
                success: function (response) {
                    if (!response.Valido) {
                        alert('Houve um erro no envio. Por favor tente mais tarde.');
                        return;
                    }
                    $(form).toggleClass('hidden', true);
                    $('.modal-footer').toggleClass('hidden', true);
                    $('.qamsgok').toggleClass('hidden', false);
                },
                error: function (xhr) {
                    var jsonResponse = JSON.parse(xhr.responseText != undefined ? xhr.responseText : "erro.");
                    console.log(jsonResponse.message);
                    alert('Houve um erro no envio. Por favor tente mais tarde.');
                }
            });
        });
        //modal qa resposta de terceiros
        $(document).on('click', '.btnOpenQAResp', function (event) {
            event.preventDefault();
            $('#idquestion').val("");
            var idquestion = parseInt($(this).data('idquestion'));
            var form = $("#formEnvioQARespostaAnalise");
            form.parsley().reset();
            form[0].reset();
            $('#idquestion').val(idquestion);
            $('#enviarQAResp').prop("disabled", false).text('Enviar resposta para revis\u00E3o');
            $('#alertaQARespComent').toggleClass('in', false);
            $('.modal-footer').toggleClass('hidden', false);
            $('.qarespmsgok').toggleClass('hidden', true);
            $('.enviarQARespErro').text("");
            $(form).toggleClass('hidden', false);
            $('#modalQAResp').modal();
        });
        //form de QA resposta de terceiros
        $(document).on('click', '.enviarQAResp', function (event) {
            event.preventDefault();
            var form = $("#formEnvioQARespostaAnalise");
            var validate = form.parsley().validate();
            if (validate == false) {
                $('.enviarQARespErro').addClass('text-danger').text('Um ou mais campos não foram preenchidos.');
            }
            if (!validate) return;
            $('.enviarQARespErro').text("");
            $(this).prop("disabled", true).text('aguarde, processando...');

            var data = $(form).serializeObject();

            $.ajax({
                type: "POST",
                url: configs.url + "/PerguntasERespostas/RespostaTerceiro",
                data: data,
                dataType: "json",
                success: function (response) {
                    if (!response.Valido) {
                        alert('Houve um erro no envio. Por favor tente mais tarde.');
                        return;
                    }
                    $(form).toggleClass('hidden', true);
                    $('.modal-footer').toggleClass('hidden', true);
                    $('.qarespmsgok').toggleClass('hidden', false);
                },
                error: function (xhr) {
                    var jsonResponse = JSON.parse(xhr.responseText);
                    console.log(jsonResponse.message);
                    alert('Houve um erro no envio. Por favor tente mais tarde.');
                }
            });
        });
        //paginacao
        $(document).on('click', '.btnAvalicaoGetPage', function () {
            var page = parseInt($(this).data('page'));
            loadAvaliacoes(page, configs.filtroAV, '');
            $('html, body').animate({
                scrollTop: $("#ancorAvp5sReviews").offset().top
            }, 300);
        });
        $(document).on('click', '.btnQAGetPage', function () {
            var page = parseInt($(this).data('page'));
            loadQA(page, configs.filtroQA, configs.QATermo);
            $('html, body').animate({
                scrollTop: $("#ancorQAp5s").offset().top
            }, 300);
        });
        //filtros                
        $(document).on('change', '#btnAvaliacaoFiltroOpcao', function () {
            var page = 1;
            var filtro = $(this).val();
            configs.filtroAV = filtro;
            loadAvaliacoes(page, filtro, '');
        });
        $(document).on('change', '#btnQAFiltroOpcao', function () {
            var page = 1;
            var filtro = $(this).val();
            configs.filtroQA = filtro;
            loadQA(page, filtro, '');
        });
        //busca por um termo
        $(document).on('click', '#btnQABuscaOpcao', function (e) {
            e.preventDefault();
            var page = 1;
            var termo = $('#txtQABusca').val();
            if (termo != undefined && termo != "") {
                configs.QATermo = termo;
                loadQA(page, configs.filtroQA, termo);
            }
        });
        //usado quando não encontra resultados na busca do cliente
        $(document).on('click', '#qaClearQuerysearch', function (e) {
            e.preventDefault();
            var page = 1;
            configs.QATermo = "";
            loadQA(page, configs.filtroQA, configs.QATermo);
        });
        $(document).on('click', '.btnthumbsupOrdown', function (e) {
            e.preventDefault();

            var idReview = $(this).data('id');
            var action = $(this).data('ac');
            var t = parseInt($(this).data('total')) + 1;

            if (action === 'positivereview') {
                $(this).html('<i class="fa fa-thumbs-up"></i> Gostei (' + t + ')');
            } else {
                $(this).html('<i class="fa fa-thumbs-down " style="color:#eb5353;"></i> Não Gostei (' + t + ')');
            }

            $('#datareview' + idReview).find('.txtinfoaction').addClass('text-success').text('Obrigado pelo seu voto!');
            $(this).attr("disabled", true); $(this).removeClass('btnthumbsupOrdown');

            updownAction(idReview, action);

        });
        $(document).on('click', '.btnReportAbuseReview', function () {
            var idReview = $(this).data('id');
            var action = $(this).data('ac');
            $('#datareview' + idReview).find('.txtinfoaction').addClass('text-danger').text('Obrigado por reportar o abuso. Iremos analisar a sua reclamação.');
            $(this).attr("disabled", true); $(this).removeClass('btnReportAbuseReview');
            console.log('btnReportAbuseReview-> action:' + action + ' idreview: ' + idReview);
            updownAction(idReview, action);
        });
        $(document).on('click', '.readmorep', function () {
            $('.commentcompletep, .commentminp').toggle();
            $(this).text($(this).text() === 'ver mais' ? 'ver menos' : 'ver mais');
        });
        $(document).on('click', '.readmoren', function () {
            $('.commentcompleten, .commentminn').toggle();
            $(this).text($(this).text() === 'ver mais' ? 'ver menos' : 'ver mais');
        });
        
        var renderContainerPrincipal = function () {
            var html = '';
            html += '<div class="container bootstrap-iso push5stars" id="push5stars">';
            if (p.DISPLAY_AVALICAO)
                html += '<div id="' + this.betaViews.configs.elAv + '">Carregando avaliações</div>';
            if (p.DISPLAY_QA)
                html += '<div id="' + this.betaViews.configs.elQA + '">Carregando perguntas e respostas</div>';
            html += '</div>';
            $(p.EL_AVALIACAO + ' > div').remove();
            $(p.EL_AVALIACAO).append(html);
        }
        var loadQA = function (page, filtro, busca) {

            if (!p.DISPLAY_QA) return;
            var rq = {
                prdCodigo: p.PRD_CODIGO,
                codigoLoja: p.LOJA_CODIGO,
                codCliente: p.APIKEY,
                pageNumber: page,
                filter: filtro,
                busca: busca
            };
            $("#" + configs.elQA).load(configs.urlstatic + "/qa?", rq, function (response, status, request) {
                this;

                if (document.getElementById("formEnvioQAAnalise") == null) {
                    $("#qaformEnvioQAAnalise").wrap('<form role="form" id="formEnvioQAAnalise" method="post">');
                }
                if (busca != "") $('#txtQABusca').val(busca);
                if (filtro != "") $('#btnQAFiltroOpcao').val(filtro);

            });
        }
        var loadAvaliacoes = function (page, filtro, busca) {
            if (!p.DISPLAY_AVALICAO) return;

            var rq = {
                prdCodigo: p.PRD_CODIGO,
                codigoLoja: p.LOJA_CODIGO,
                Authorization: p.APIKEY,
                ActualPageNumber: page,
                Filtro: filtro,
                busca: busca,
            };

            $("#" + configs.elAv).load(configs.urlstatic + "/avaliacaoproduto?", rq, function (response, status, request) {
                this;

                if (document.getElementById("formEnvioAvaliacaoAnalise") == null) {
                    $("#avformEnvioAvaliacaoAnalise").wrap('<form role="form" id="formEnvioAvaliacaoAnalise" method="post">');
                }

                if (filtro != "") $('#btnAvaliacaoFiltroOpcao').val(filtro);

                if (response != undefined) {
                    widgetStarDisplay();
                }
            });


        }
        var updownAction = function (idreview, typeSubmit) {
            var action = "";
            if (typeSubmit === 'positivereview') {
                action = "/PostarComentarioPositivo";
            }
            else if (typeSubmit === 'negativereview') {
                action = "/PostarComentarioNegativo";
            }
            else if (typeSubmit === 'reviewreportabuse') {
                action = "/ReportarAbuso"
            }
            $.post(configs.url + "/avaliacoes/enviar" + action + "?idreview=" + idreview, function (data, textStatus, jqXHR) {
                console.log(data);
            }, "dataType"
            );
        }
        var widgetStarDisplay = function () {

            var score = $('#p5sScore').attr('data-average-score');
            var percentRecomend = $('.percentrecomendaprd').attr('data-percent-recomenda-prod');
            var div = '';
            div += '<div class="bootstrap-iso push5stars" id="displaytopratingsp5s">';
            if (score != undefined) {                
                div += '<span class="average">' + score + '</span>';
                div += '<a href="#painelavaliacoescomlist" tittle="ver avaliações de clientes"><div  id="p5sScore3" class="p5s-rating-display stars1 " data-average-score="' + score + '"></div></a>';
                div += '<br class="clearfix">';                
                div += '<br class="clearfix">';
                div += '<span><a href="#painelavaliacoescomlist">ver avaliações</a></span>'                
            } else {
                div += '<a href="#" class="btn btn-default btnOpenAvaliacao trigger">Faça uma Avaliação</a>'
            }
            if (percentRecomend) {
                div += '<br />'
                div += '<div><i class="fa fa-check-circle text-success" aria-hidden="true"></i> ' + percentRecomend + '% dos clientes recomendam este produto!</div>'
            }            
            div += '<br />'
            div += '</div>'

            $(p.EL_AVALIACAO_RATING + ' > div').remove();
            $(p.EL_AVALIACAO_RATING).append(div);

        }

        

        if (this.configs.apistatus) {
            renderContainerPrincipal();
            loadQA(1, '', '');
            loadAvaliacoes(1, '', '');
        }

    }
};

window.betaviews_Init && betaViews.init(betaviews_Init);
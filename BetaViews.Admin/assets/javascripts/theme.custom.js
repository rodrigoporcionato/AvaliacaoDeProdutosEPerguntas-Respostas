//modal de confirmação

$(document).on('click', '.modalconfirm', function (e) {
    var id = $(this).data('id');    
    $('.modal-confirm').attr('data-id', id);

    $.magnificPopup.open({
        items: {
            src: '#modalConfirm'
        },
        type: 'inline',
        fixedContentPos: false,
        fixedBgPos: true,
        overflowY: 'auto',
        closeBtnInside: true,
        preloader: false,
        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-slide-bottom',
        modal: true
    });
});
$(document).on('click', '.modal-dismiss', function (e) {
    e.preventDefault();
    $.magnificPopup.close();
});


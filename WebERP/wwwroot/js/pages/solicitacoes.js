$(document).ready(function () {
    $("#solicitacoes-table").DataTable();

    $(".js-negar-solicitacao").click(function (e) {
        var solicitacaoId = $(e.target).attr("data-solicitacao-id");
        mudarStatusSolicitacao("Negar", solicitacaoId);
    });

    $(".js-aprovar-solicitacao").click(function (e) {
        var solicitacaoId = $(e.target).attr("data-solicitacao-id");
        mudarStatusSolicitacao("Aprovar", solicitacaoId);
    });

    $(".js-adicionar-orcamento").click(function (e) {
        var solicitacaoId = $(e.target).attr("data-solicitacao-id");
        abrirModalOrcamentos(solicitacaoId);
    });
});

function mudarStatusSolicitacao(status, id) {
    var sendData = { "solicitacaoId": id };
    var baseUrl = "/api/Solicitacao/" + status + "Solicitacao";

    swal({
        title: status + " solicitação?",
        text: "Confirme a alteração na solicitação.",
        type: "info",
        showCancelButton: true,
        confirmButtonText: "Sim, " + status.toLowerCase() + " a solicitação!",
        cancelButtonText: "Não, cancele por favor!",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: baseUrl,
                type: 'POST',
                data: sendData,
                success: function () {
                    swal({
                        title: "Alterado!",
                        text: "A solicitação foi alterada com sucesso.",
                        type: "success"
                    }, function (isConfirm) {
                        location.reload();
                    });
                },
                error: function () {
                    swal({
                        title: "Algo deu errado. :(",
                        text: "Algo aconteceu enquanto a operação era executada.",
                        type: "error"
                    });
                }
            });
        } else {
            swal("Cancelado", "A solicitação não foi alterada, não se preocupe. :)", "error");
        }
    });
}

function abrirModalOrcamentos(solicitacaoId) {
    var url = "/Solicitacao/Orcamentos/" + solicitacaoId;
    $.get(url, function (data) {
        $("#modal-orcamentos-content").html(data);
        $("#modal-orcamentos").modal("show");
    });

    $('#modal-orcamentos').on('hidden.bs.modal', function () {
        $("#modal-orcamentos-content").html("");
    });
}
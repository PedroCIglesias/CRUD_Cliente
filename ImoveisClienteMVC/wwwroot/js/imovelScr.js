

$(document).ready(function () {
    $('#update').hide();
    ViewTable(localStorage.getItem('clienteId'));
    $('#clienteId').val(localStorage.getItem('clienteId'));
});

function ViewTable(clienteId) {
    $.ajax({
        type: "POST",
        url: "Imovel/ViewTable",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(clienteId),
        success: function (json1) {
            debugger;
            var tableLoad = json1.html;
            var dataSet = eval("[" + tableLoad + "]");
            $('#viewValues').DataTable({
                ordering: false,
                dom: 'lrt',
                "bLengthChange": false,
                "bPaginate": false,
                data: dataSet,
                columns: [
                    { title: "ID" },
                    { title: "Cliente" },
                    { title: "Nome Imóvel" },
                    { title: "Valor" },
                    { title: "Cidade" },
                    { title: "Bairro" },
                    { title: "Logradouro" },
                    { title: "Numero" },
                    { title: "Data Cadastro" },
                    { title: "Data Atualização" },
                    { title: "Ação" },
                ],
            });
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
};

function fnInsert() {
    var model = {
        ClienteId: $("#clienteId").val(),
        Nome: $("#nomeImovel").val(),
        Valor: $("#valor").val(),
        Cidade: $("#cidade").val(),
        Bairro: $("#bairro").val(),
        Logradouro: $("#logradouro").val(),
        Numero: $("#numero").val()
    };

    $.ajax({
        type: "POST",
        url: "Imovel/Insert",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(model),
        success: function (json) {
            window.location.replace("/Imovel");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function filterGlobal() {
    $('#viewValues').DataTable().search(
        $('#pesquisa').val(),
    ).draw();
}

function fnEdit(imovelId) {
    $('#addImovelModal').modal('toggle');
    $("#imovelId").val(imovelId);

    $.ajax({
        type: "POST",
        url: "Imovel/Edit",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(imovelId),
        success: function (json) {
            var arrval = json.htmlValues;

            $("#clienteId").val(arrval[0]);
            $("#nomeImovel").val(arrval[1])
            $("#valor").val(arrval[2]);
            $("#cidade").val(arrval[3]);
            $("#bairro").val(arrval[4]);
            $("#logradouro").val(arrval[5]);
            $("#numero").val(arrval[6]);

            $('#insert').hide();
            $('#update').show();

        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function fnUpdate() {
    var model = {
        ImovelId: $("#imovelId").val(),
        ClienteId: $("#clienteId").val(),
        Nome: $("#nomeImovel").val(),
        Valor: $("#valor").val(),
        Cidade: $("#cidade").val(),
        Bairro: $("#bairro").val(),
        Logradouro: $("#logradouro").val(),
        Numero: $("#numero").val()
    };

    $.ajax({
        type: "POST",
        url: "Imovel/Update",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(model),
        success: function (json) {
            window.location.replace("/Imovel");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function fnDelete(clienteId) {
    $.ajax({
        type: "POST",
        url: "Imovel/Delete",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(clienteId),
        success: function (json) {
            window.location.replace("/Imovel");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}


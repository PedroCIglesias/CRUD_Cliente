

$(document).ready(function () {
    $('#update').hide();
    //validaCampos();
    ViewTable();
});

/*function validaCampos(){
    $("#formAddCliente").validate({
        rules: {
            nome: {
                required:true
            }
        }
    })
}*/

function fnPopula() {
    $.ajax({
        type: "POST",
        url: "Cliente/Popula",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(),
        success: function (json) {
            window.location.replace("/Cliente");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function ViewTable() {
    $.ajax({
        type: "POST",
        url: "Cliente/ViewTable",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(),
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
                    { title: "Nome" },
                    { title: "Tipo Pessoa" },
                    { title: "Telefone" },
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
        Nome: $("#nomeCliente").val(),
        Tipo: $("input[name='tipoCliente']:checked").val(),
        TelefoneContato: $("#telefoneContato").val()
    };

    $.ajax({
        type: "POST",
        url: "Cliente/Insert",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(model),
        success: function (json) {
            window.location.replace("/Cliente");
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

function fnEdit(clienteId) {
    $('#addClienteModal').modal('toggle');

    $.ajax({
        type: "POST",
        url: "Cliente/Edit",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(clienteId),
        success: function (json) {
            var arrval = json.htmlValues;

            

            $("#nomeCliente").val(arrval[0]);
            $('input[name="tipoCliente"][value="' + arrval[1].toUpperCase() +'"]').attr('checked', true);
            $("#telefoneContato").val(arrval[2]);
            $("#clienteId").val(arrval[3]);

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
        Nome: $("#nomeCliente").val(),
        Tipo: $("input[name='tipoCliente']:checked").val(),
        TelefoneContato: $("#telefoneContato").val(),
        ClienteId: $("#clienteId").val()
    };

    $.ajax({
        type: "POST",
        url: "Cliente/Update",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(model),
        success: function (json) {
            window.location.replace("/Cliente");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function fnDelete(clienteId) {
    $.ajax({
        type: "POST",
        url: "Cliente/Delete",
        datatype: "json",
        contentType: "application/json: charset=utf-8",
        data: JSON.stringify(clienteId),
        success: function (json) {
            window.location.replace("/Cliente");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function fnImovel(clienteId) {
    localStorage.setItem('clienteId', clienteId);
    window.location.replace("/Imovel");
}


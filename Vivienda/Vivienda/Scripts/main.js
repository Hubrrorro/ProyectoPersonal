$(document).on({
    ajaxStart: function () {
        $('#modalWait').modal('show');
    },
    ajaxStop: function () {
        setTimeout(function () {
            $('#modalWait').modal('hide');
        }, 500);
    }
});
function FncAjax(modelo, url, tipo, funcion, asincrono) {
    if (asincrono == null) {
        asincrono = true
    }
    $.ajax({
        url: url, data: modelo,  type: tipo, async: asincrono,
        success: function (result) {
            funcion(result);
        },
        error: function (jqXHR, status, err) {
            console.log(jqXHR);
            console.log(status);
            console.log(err);
        }
    });
}
function FncMensaje(titulo, mensaje, tipo) {
    $('#modalMensajeTitulo').html(titulo);
    var msn = "";
    if (tipo) {
        msn += "<i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i>";
    }
    else {
        msn += "<i class=\"fa fa-exclamation-circle\" aria-hidden=\"true\"></i>";
    }
    msn += "  ";
    msn += mensaje;
    $('#modalMensajeBody').html(msn);
    $('#modalMensaje').modal('show');
}
function FncConsultarTabla(result) {
    if (!result.Resultado) {
        FncMensaje('Mensaje Vivienda', result.Mensaje, result.Resultado);
        return;
    }
    $('.table').fadeIn();
    $('.table').bootstrapTable('destroy');
    $('.table').bootstrapTable({ columns: result.Datos.ColumnaPost, data: result.Datos.Datos });
    $('.table').bootstrapTable('refresh')
}
function FncDatosRequeridos() {
    var isExiste = false;
    $('.valida').each(function () {
        if ($(this).val().trim() === "") {
            $(this).addClass("is-invalid");
            isExiste = true;
        }
        else
            $(this).removeClass("is-invalid");
    });
    return isExiste;
}
function GetInfoDLL(url,control,filtro, valor) {
    //var modelo = new Object();
    //modelo.Activo = 1;
    $('#' + control).html('');
    if (filtro)
        $('#' + control).append('<option value="-1" >Todos</option>');
    else
        $('#' + control).append('<option value="-1" disabled>Selecciona</option>');
    $.ajax({
        url: url,  type: 'GET', async: true,
        success: function (result) {
            if (result.Resultado) {
                $.each(result.Datos, function (key, value) {
                    $('#' + control).append('<option value="' + value.Id + '" >' + value.Descripcion + '</option>');
                });
            }
            $('#' + control).val(valor)
        },
        error: function (jqXHR, status, err) {
            console.log(jqXHR);
            console.log(status);
            console.log(err);
        }
    });
}

$(document).ready(function () {
    $("#MarcaId").change(function () {
        var selectedMarca = $(this).val();
        if (selectedMarca) {
            $.ajax({
                type: "GET",
                url: '/Aviones/GetModelosByMarca?idMarca=' + selectedMarca,
                data: { idMarca: selectedMarca },
                success: function (data) {
                    var modelosDropdown = $("#Modelo");
                    modelosDropdown.empty();
                    modelosDropdown.append($('<option></option>').val('').text('Seleccione un modelo'));
                    $.each(data, function (index, item) {
                        modelosDropdown.append($('<option></option>').val(item.Value).text(item.Text));
                    });
                    $("#modeloField").show();
                },
            });
        } else {
            $("#modeloField").hide();
        }
    });
});
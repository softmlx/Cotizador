document.querySelector('#btnGuardarContacto').addEventListener('click', function () {
    var cTitulo = document.querySelector('#txtTituloContacto').value,
        cNombre = document.querySelector('#txtNombreContacto').value,
        cApellidos = document.querySelector('#txtApellidosContacto').value,
        cTelefono = document.querySelector('#txtTelefonoContacto').value,
        cExtension = document.querySelector('#txtExtensionContacto').value,
        cCelular = document.querySelector('#txtCelularContacto').value,
        cCorreo = document.querySelector('#txtCorreoContacto').value,
        cPuesto = document.querySelector('#txtPuestoContacto').value;

    addContact(cTitulo, cNombre, cApellidos, cTelefono, cExtension, cCelular, cCorreo, cPuesto);
    drawContactTable();
    clear('txtTituloContacto', 'txtNombreContacto', 'txtApellidosContacto', 'txtTelefonoContacto', 'txtExtensionContacto', 'txtCelularContacto', 'txtCorreoContacto', 'txtPuestoContacto');
    hideFormC();
});

document.querySelector('#btnGuardarInfo').addEventListener('click', function () {
    var sRazonSocial = document.querySelector('#txtRazonSocial_IF').value,
        sRFC = document.querySelector('#txtRFC_IF').value,
        sCalle = document.querySelector('#txtCalle_IF').value,
        sNumeroExterior = document.querySelector('#txtNumeroExterior_IF').value,
        sNumeroInterior = document.querySelector('#txtNumeroInterior_IF').value,
        sCodigoPostal = document.querySelector('#txtCodigoPostal_IF').value,
        sColonia = document.querySelector('#txtColonia_IF').value,
        sCiudad = document.querySelector('#txtCiudad_IF').value,
        sMunicipio = document.querySelector('#comboboxMunicipioI').value,
        sEstado = document.querySelector('#comboboxEstadoI').value,
        sPais = document.querySelector('#comboboxPaisI').value;

    addInformacionFiscal(sRazonSocial, sRFC, sCalle, sNumeroExterior, sNumeroInterior, sCodigoPostal, sColonia, sCiudad, sMunicipio, sEstado, sPais);
    drawInfoTable();
    clear('txtRazonSocial_IF', 'txtRFC_IF', 'txtCalle_IF', 'txtNumeroExterior_IF', 'txtNumeroInterior_IF', 'txtCodigoPostal_IF', 'txtColonia_IF', 'txtCiudad_IF');
    hideFormInf();
});

document.querySelector('#btnGuardar').addEventListener('click', function () {
    var cteNombre = document.querySelector('#txtNombre_Cte').value,
        cteTelefono = document.querySelector('#txtTelefono_Cte').value,
        cteExtension = document.querySelector('#txtExtension_Cte').value,
        cteCelular = document.querySelector('#txtCelular_Cte').value,
        cteCalle = document.querySelector('#txtCalle_Cte').value,
        cteNumeroE = document.querySelector('#txtNumeroE_Cte').value,
        cteNumeroI = document.querySelector('#txtNumeroI_Cte').value,
        cteCodigoP = document.querySelector('#txtCodigoP_Cte').value,
        cteColonia = document.querySelector('#txtColonia_Cte').value,
        cteCiudad = document.querySelector('#txtCiudad_Cte').value,
        cteMunicipio = document.querySelector('#comboboxMunicipio').value,
        cteEstado = document.querySelector('#comboboxEstado').value,
        ctePais = document.querySelector('#comboboxPais').value;

    addCliente(cteNombre, cteTelefono, cteExtension, cteCelular, cteCalle, cteNumeroE, cteNumeroI, cteCodigoP, cteColonia, cteCiudad, cteMunicipio, cteEstado, ctePais);
});

document.querySelector('#btnAgregarContacto').addEventListener('click', function () {

    document.getElementById("formularioC").style.display = "";
});

document.querySelector('#btnAgregarInfoF').addEventListener('click', function () {

    document.getElementById("formularioIF").style.display = "";
});

function clear(field0, field1, field2, field3, field4, field5, field6, field7) {
    document.getElementById(field0).value = '';
    document.getElementById(field1).value = '';
    document.getElementById(field2).value = '';
    document.getElementById(field3).value = '';
    document.getElementById(field4).value = '';
    document.getElementById(field5).value = '';
    document.getElementById(field6).value = '';
    document.getElementById(field7).value = '';
}

function hideFormC() {
    document.getElementById("formularioC").style.display = "none";
}

function hideFormInf() {
    document.getElementById("formularioIF").style.display = "none";
}

function drawContactTable() {
    var list = getContactList(),
        tbody = document.querySelector('#tablaContacto tbody');

    tbody.innerHTML = '';

    for (var i = 0; i < list.length; i++) {
        var row = tbody.insertRow(i),
            tituloCell = row.insertCell(0),
            nombreCell = row.insertCell(1),
            apellidosCell = row.insertCell(2),
            telefonoCell = row.insertCell(3),
            extensionCell = row.insertCell(4),
            celularCell = row.insertCell(5),
            correoCell = row.insertCell(6),
            puestoCell = row.insertCell(7);

        tituloCell.innerHTML = list[i].Titulo;
        nombreCell.innerHTML = list[i].Nombre;
        apellidosCell.innerHTML = list[i].Apellidos;
        telefonoCell.innerHTML = list[i].Telefono;
        extensionCell.innerHTML = list[i].Extension;
        celularCell.innerHTML = list[i].Celular;
        correoCell.innerHTML = list[i].CorreoElectronico;
        puestoCell.innerHTML = list[i].Puesto;

        tbody.appendChild(row)
    }

}

function drawInfoTable() {
    var list = getInformacionFiscal(),
        tbody = document.querySelector('#tablaInformacionF tbody');

    tbody.innerHTML = '';

    for (var i = 0; i < list.length; i++) {
        var row = tbody.insertRow(i),
            razonCell = row.insertCell(0),
            rfcCell = row.insertCell(1);

        razonCell.innerHTML = list[i].RazonSocial;
        rfcCell.innerHTML = list[i].RFC;

        tbody.appendChild(row)
    }
}
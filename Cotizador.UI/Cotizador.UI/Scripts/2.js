var contactsList = [],
    infoFList = [];

function addContact(contactTitulo, contactNombre, contactApellidos, contactTelefono, contactExtension, contactCelular, contactCorreo, contactPuesto)
{
    var newContact =
    {
        Titulo: contactTitulo,
        Nombre: contactNombre,
        Apellidos: contactApellidos,
        Telefono: contactTelefono,
        Extension: contactExtension,
        Celular: contactCelular,
        CorreoElectronico: contactCorreo,
        Puesto: contactPuesto
    };
    contactsList.push(newContact);

    //var myString2 = JSON.stringify(newContact);
    //console.log(myString2);
}

function getContactList()
{
    return contactsList;
}

function addCliente(clienteNombre, clienteTelefono, clienteExtension, clienteCelular, clienteCalle, clienteNumeroE, clienteNumeroI, clienteCodigo, clienteColonia, clienteCiudad, clienteMunicipioId, clienteEstadoId, clientePaisId)
{
    var newUbicacionCliente =
    {
        MunicipioId: clienteMunicipioId,
        EstadoId: clienteEstadoId,
        PaiId: clientePaisId
    };

    var newDireccionCliente =
    {
        Calle: clienteCalle,
        NumeroExterior: clienteNumeroE,
        NumeroInterior: clienteNumeroI,
        CodigoPostal: clienteCodigo,
        Colonia: clienteColonia,
        Ciudad: clienteCiudad,
        UbicacionGeografica: newUbicacionCliente
    };

    var newCliente =
    {
        Nombre: clienteNombre,
        Telefono: clienteTelefono,
        Extension: clienteExtension,
        Celular: clienteCelular,
        Direccion: newDireccionCliente,
        ContactoCliente: getContactList(),
        InformacionFiscal: getInformacionFiscal()
    };

    myString = JSON.stringify(newCliente);
    console.log(myString);
}

function addInformacionFiscal(InfoFRazon, InfoFRfc, InfoFCalle, InfoFNumeroe, InfoFNumeroi, InfoFCodigo, InfoFColonia, InfoFCiudad, InfoFMunicipioId, InfoFEstadoId, InfoFPaisId)
{
    var newUbicacionInfo =
    {
        MunicipioId: InfoFMunicipioId,
        EstadoId: InfoFEstadoId,
        PaiId: InfoFPaisId
    };

    var newDireccionInfo =
    {
        Calle: InfoFCalle,
        NumeroExterior: InfoFNumeroe,
        NumeroInterior: InfoFNumeroi,
        CodigoPostal: InfoFCodigo,
        Colonia: InfoFColonia,
        Ciudad: InfoFCiudad,
        UbicacionGeografica: newUbicacionInfo
    };

    var newInformacionFiscal =
    {
        RazonSocial: InfoFRazon,
        RFC: InfoFRfc,
        Direccion: newDireccionInfo
    };

    infoFList.push(newInformacionFiscal);
}

function getInformacionFiscal()
{
    return infoFList;
}
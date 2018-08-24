

function getSocialNetworkList() {

    $.ajax({
        method: "POST",
        url: UrlGetSocialNetworkList,
        success: function (result)
        {
            if (result)
            {
                addFavouriteNets(result, '#FavouriteNetWork');
                addNetworks(result);
            }
        }
    })
}


function addFavouriteNets(data, itemId)
{
    $.each(data, function (i, item) {
        $(itemId).append($('<option>', {
            value: item.id,
            text: item.name
        }));
    });
}

function addNetworks(data)
{
    $.each(data, function (i, item) {
        $('#Networks').append($('<div />', {
            class: 'form-check',
            id: i
        }));
        $('#' + i).append($('<label />', {
            id: 'label' + i,
            class: "form-check-label",
            text: item.name + ": "
        }));
        $('#label' + i).append($('<input />', {
            type: 'checkbox',
            value: item.id,
            name: "Netwoks"
        }));
    });
}







function getEditSocialNetworkList() {

    $.ajax({
        method: "POST",
        url: UrlGetSocialNetworkList,
        success: function (result) {
            if (result) {
                addFavouriteNets(result, '#FavouriteNetWork');
                addFavouriteNets(result, '#addNetWork');
            }
        }
    })
}














function getUserData()
{
    $.ajax({
        method: "GET",
        url: UrlGetUserData,
        data: { "Id": UserId },
        dataType: 'json',
        success: function (result) {
            if (result) {
                addUserData(result)
            }
        }
    })
}

function addUserData(data)
{
    $("#userId").append(data.Id);
    $("#nombre").append(data.Name);
    $("#apellido").append(data.Subname);
    $("#favorita").append(data.FavouriteNetwork.Name);
    $.each(data.Networks, function (i, item) {
        $("#NetList").append($('<li />', {
            text: "red social: " + item.name
        }));
    });
}
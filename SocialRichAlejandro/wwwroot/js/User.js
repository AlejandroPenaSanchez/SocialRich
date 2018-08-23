

function getSocialNetworkList() {

    $.ajax({
        method: "POST",
        url: UrlGetSocialNetworkList,
        success: function (result)
        {
            if (result)
            {
                addFavouriteNets(result);
                addNetworks(result);
            }
        }
    })
}


function addFavouriteNets(data)
{
    $.each(data, function (i, item) {
        $('#FavouriteNetWork').append($('<option>', {
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


function getUserData()
{
    $.ajax({
        method: "POST",
        url: UrlGetUserData,
        data: UserId,
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
    $.each(data.Networks, function (i, item)
    {
        $("#NetList").append($('<li />', {
            text: "red social: " + item.name
        }));
    }
}
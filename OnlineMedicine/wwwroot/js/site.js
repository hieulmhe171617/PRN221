$(() => {
    LoadMedicinesData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadMedicines", function () {
        LoadMedicinesData();
    });
});

function LoadMedicinesData() {
    var div = '';
    $.ajax({
        url: '/Medicine/GetMedicines',
        method: 'GET',
        success: (result) => {
            $.each(result, (k, v) => {
                div += `
                <tr>
                    <td>${v.name}</td>
                    <td><img style="height: 50px" src="${v.image}"></td>
                    <td>${v.categoryName}</td>
                    <td>${v.typeName}</td>
                    <td>${v.countryName}</td>
                    <td>${v.price}</td>
                    <td>${v.quantity}</td>
                    <td>${v.expiredDate}</td>
                    <td>${v.minAge}</td>
                    <td>
                        <a href="/Medicine/Edit?mid=${v.id}">Edit</a>
                        <a href="/Medicine/Delete?mid=${v.id}">Delete</a>
                    </td>
                </tr>`;
            });
            $("#tableBody").html(div);
        },
        error: (error) => {
            console.log(error);
        }
    });
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function printMsg(event) {
    //var cities = document.getElementById('cities');

    $('#cities').empty();
    var e = document.getElementById("my-select");
    var value = e.value;
    fetch("https://localhost:7285/api/Villages/GetVillages/" + value)
        .then((response) => response.json()) //parse json data
        .then(function (todos) {
            var optDefault = document.createElement('option');
            optDefault.value = 0;
            optDefault.innerHTML = 'Lütfen bir avm seçiniz';
            optDefault.disabled = true;
            optDefault.selected = true;
            document.getElementById('cities').appendChild(optDefault);
            todos.forEach((todo) => {
                var opt = document.createElement('option');
                opt.value = todo.id;
                opt.innerHTML = todo.name;
                document.getElementById('cities').appendChild(opt);
            });
        });
}

function sendVillage(event) {
    var e = document.getElementById("cities");
    var value = e.value;
    var inputVillage = document.getElementById("villageId");
    inputVillage.value = value;
}
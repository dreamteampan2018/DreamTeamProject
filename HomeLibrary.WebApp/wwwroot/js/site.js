// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const getName = (name,surname) =>
{

    $("#basicModal").show(function () {
        $(this).find("#myModalLabel").text(name+" "+surname)
        });
        
} 
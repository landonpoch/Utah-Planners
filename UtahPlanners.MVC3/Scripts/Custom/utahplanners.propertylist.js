/// <reference path="../jquery-1.4.4-vsdoc.js" />

var confirmationMessage = "Are you sure you want to delete this property?";
$('a.delete').click(function (event) {
    event.preventDefault();
    if (confirm(confirmationMessage)) {
        var identifier = $(this).attr("data-identifier");
        var input = $("<input>")
                    .attr("type", "hidden")
                    .attr("name", "id")
                    .val(identifier);
        var form = $('form');
        form.append(input);
        form.submit();
    }
});
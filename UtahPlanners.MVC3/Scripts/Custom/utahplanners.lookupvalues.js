/// <reference path="../jquery-1.4.4-vsdoc.js" />

$(document).ready(function () {
    registerEvents();
});

function registerEvents() {
    $('td>a[data-command=submit]').click(function (event) {
        var row = initializeCommand(this, event);
        $.ajax({
            url: getModifyUrl(),
            type: 'post',
            data: $('form').serializeArray(),
            success: function (response, status, jqXHR) {
                $('#result').text(response);
                updateSuccessful(row);
            },
            error: function (jqXHR, status, error) {
                $('#result').text('An error occured while submitting your request');
            },
            complete: function (jqXHR, status, error) {
                toggleRow(row);
            }
        });
    });

    $('td>a[data-command=delete]').click(function (event) {
        var row = initializeCommand(this, event);
        if (confirm('Are you sure you want to delete this item?')) {
            $.ajax({
                url: getDeleteUrl(),
                type: 'post',
                data: $('form').serializeArray(),
                success: function (response, status, jqXHR) {
                    $('#result').text(response);
                    row.remove();
                },
                error: function (jqXHR, status, error) {
                    $('#result').text('');
                }
            });
        }
    });

    $('td>a[data-command=modify]').click(function (event) {
        var row = initializeCommand(this, event);
        toggleRow(row);
    });

    $('td>a[data-command=cancel]').click(function (event) {
        var row = initializeCommand(this, event);
        toggleRow(row);
    });
}

function toggleRow(row) {
    row.find('td').children().each(function () {
        $(this).toggle();
    });
}

function initializeCommand(linkClicked, event) {
    event.preventDefault();
    var key = $(linkClicked).parent().parent().attr('data-key');
    $('#SelectedId').val(key);
    var row = $('tr[data-key=' + key + ']');
    return row;
}

function getModifyUrl() {
    var postUrl = 'ModifyCode';
    if (window.location.pathname.indexOf('Types') != -1) {
        postUrl = 'ModifyType';
    }
    return postUrl;
}

function getDeleteUrl() {
    var postUrl = 'DeleteCode';
    if (window.location.pathname.indexOf('Types') != -1) {
        postUrl = 'DeleteType';
    }
    return postUrl;
}

function setColumnValues(row, description) {
    var column = row.find('td[data-column=' + description + ']');
    var value = column.find('input[type=text]').val();
    column.find('span').text(value);
}
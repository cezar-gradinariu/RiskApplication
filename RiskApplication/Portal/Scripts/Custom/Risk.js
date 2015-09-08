$(document).ready(function () {
    $('#statistics > tbody > tr').on('click', function () {
        $('#statistics > tbody > tr').removeClass('selectedRow');
        $(this).addClass('selectedRow');
        $('#settledBets').load('/Risk/GetSettledBet/' + $(this).find('td').first().text(),
        function( response, status, xhr ) {
            if (status == "error") {
                $('#settledBets').empty();
                var msg = "We encountered an error while retrieveing data from the server!";
                $('#settledBets').html(msg + xhr.status + " " + xhr.statusText);
            }
        });
    });
});
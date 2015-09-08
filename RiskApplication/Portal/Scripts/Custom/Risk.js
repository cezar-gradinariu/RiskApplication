$(document).ready(function () {
    $('#statistics > tbody > tr').on('click', function () {
        $('#statistics > tbody > tr').removeClass('selectedRow');
        $(this).addClass('selectedRow');
        $('#settledBets').load('/Risk/GetSettledBet/' + $(this).find('td').first().text());
    });
});
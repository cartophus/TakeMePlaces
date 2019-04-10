var logID = 'log',
    log = $('<div id="' + logID + '"></div>');
$('body').append(log);
$('[type*="submit"]').change(function () {
    var me = $(this);
    log.html(me.attr('value'));
});
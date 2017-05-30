$(function() {
    /*var pull = $('#pull');
    menu = $('ul.nav');
    menuHeight = menu.height();

    $(pull).on('click', function(e) {
        e.preventDefault();
        menu.slideToggle();
    });

    $(window).resize(function() {
        var w = $(window).width();
        if (w > 320 && menu.is(':hidden')) {
            menu.removeAttr('style');
        }
    });*/
    Galleria.loadTheme('../../Libs/galleria/dist/themes/classic/galleria.classic.min.js');
    Galleria.run('.galleria');
});

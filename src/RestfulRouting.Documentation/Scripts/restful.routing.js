$(function () {
    window.prettyPrint() && prettyPrint();

    // Disable certain links in docs
    $('section [href^=#]').click(function(e) {
        e.preventDefault();
    });
});


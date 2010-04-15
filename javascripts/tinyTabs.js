(function($) {
    $.fn.tinyTabs = function(options) {
        options = $.extend({}, $.fn.tinyTabs.defaults, options);

        var elements = this.each(function() {
            var tabs = $(this).find(".tab");
            tabs.hide().filter(":first").show();
            var tabAnchors = $(this).find(" > ul a");
            tabAnchors.click(function() {
                tabs.hide();
                tabs.filter(this.hash).show();
                tabAnchors.removeClass("selected");
                $(this).addClass("selected");
                return false;
            });
            // $(tabAnchors.get(1)).click();
        });
        return elements;
    };
    $.fn.tinyTabs.defaults = {};
})(jQuery);
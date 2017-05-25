"use strict";
$.getScript("/js/beautifiers.js", function onBeautifiersLoaded() {
    $(document).ready(
        function () {
            postHighlighting();
        });
});

function postHighlighting() {
    let target = $(".post");
    beautifiers.beautify(target);
};
"use strict";
$.getScript("/js/beautifiers.js", function onBeautifiersLoaded() {
    $(document).ready(postHighlighting);
});

function postHighlighting() {
    let target = $(".post");
    beautifiers.beautify(target);
};
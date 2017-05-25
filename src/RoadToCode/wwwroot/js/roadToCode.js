"use strict";
$.getScript("/js/responsiveImager.js", function onResponsiveLoad() {
    beautifiers.replaceWithImages = imager.replaceImages;
    roadToCode();

});


function roadToCode() {
    let firstObservedId = "init";
    let sheetDiv = $("#sheet");
    let lastSheetSelector = '#sheet >:last-child';
    var scrollFireSettings = [];
    var currentPost = 0;
    var whenReceived = function (html, status) {
        if (status == 'success') {
            let i = currentPost;
            currentPost = currentPost + 1;
            let post = `<div id="post${i}">${html}</div>`;
            var input = $($.parseHTML(post, null, false));
            let beautifed = beautifiers.beautify(input);
            var div = sheetDiv.append(beautifed);
            scrollFireSettings.push({
                selector: lastSheetSelector,
                offset: 0,
                callback: function fetchNext() {
                    fetchPost()
                }
            });
            Materialize.scrollFire(scrollFireSettings);
        } else if (status == 'nocontent') {
            Materialize.toast('That\'s all :) Visit me later for more content.', 6000);
        } else {
            Materialize.toast('I was not able to fetch next post from server. Could you reload page, please?', 3000);
            Materialize.toast('If this message is showing regullary please fell free to contact an administarator.', 4000);
        }
    };
    let fetchPost = function () {
        let onData = function (html, status, jqxhr) {
            whenReceived(html, status);
        };
        $.get(window.location.href + `Blog/Post/${currentPost}/true`)
            .done(onData);
    }
    fetchPost();
};
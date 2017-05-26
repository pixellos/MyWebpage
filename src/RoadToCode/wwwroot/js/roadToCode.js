"use strict";
/**
 * jQuery object
 * @external jQuery
 * @see {@link http://api.jquery.com/jQuery/}
 */

/** 
 */
Array.prototype.stringDistinct = function () {
    let temp = [];
    this.forEach(function (item) {
        function weakEqual(q) { return q.toUpperCase() === item.toUpperCase(); };
        if (temp.findIndex(weakEqual) == -1) {
            temp.push(item);
        }
    });
    return temp;
}

$.getScript("/js/responsiveImager.js", function onResponsiveLoad() {
    beautifiers.replaceWithImages = imager.replaceImages;
    roadToCode();
    categoriesManagament();
});

let categoriesManagament = function () {
    /**
     * @param  {string} data
     */
    let whenDone = function (data) {
        let splitted = data.stringDistinct().map(categoriesManagament.transform).reduce(function (a, b) {
            return a.concat(b);
        });
        $("#categories").html(splitted);
        $('ul.tabs').tabs()
        $('ul.tabs > li').click(function () {
            console.log($(this));
            categoriesManagament.showOf($(this)[0].textContent)
        });
    }
    $(document).ready(function () {
        $.ajax({
            url: "Blog/Categories",
            type: "get",
            success: whenDone
        });
    });
};

/**
 * Implementation should hide all posts without {tag} and should expose all post with it
 * @param {string} tag Searched tag
 */
categoriesManagament.showOf = function (tag) {
    let posts = $(`.card-panel.hoverable`);
    let toHide = posts.filter(function () {
        return categoriesManagament.filter(tag, $(this))
    });
    let toShow = posts.not(toHide);
    toHide.slideUp();
    toShow.slideDown();
};

/**
 * @param  {string} tag Searched tag
 * @param  {module:jQuery} element
 */
categoriesManagament.filter = function (tag, element) {
    let regexp = new RegExp(`${tag}(;|$)`,'i');
    let tagValue = element.attr('tag');
    let result = regexp.exec(tagValue) == null;
    return result
};

/**
 * Transform text into html
 * @param  {string} text
 * @return {string} li 
 */
categoriesManagament.transform = function transform(text) {
    let pattern = `<li class="tab col"><a href="" id="${text}">${text}</a></li>`
    return pattern;
};

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
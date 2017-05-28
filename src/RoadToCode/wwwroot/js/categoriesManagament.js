"use strict";

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
        //$('ul.tabs > li').click(function () {
            console.log($(this));
            categoriesManagament.showOf($(this)[0].textContent)
        };
        categoriesManagament.dataSource(whenDone);
};

/**
 * @param  {function(string))} whenDone
 */
categoriesManagament.dataSource = function (whenDone)
{
    let data = $.ajax({
            url: "Blog/Categories",
            type: "get",
            success: whenDone
    });
} 

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
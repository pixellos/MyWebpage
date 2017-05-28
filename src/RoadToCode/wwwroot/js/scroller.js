"use strict";

let scroller = {}

scroller.currentContainerSeeker = function (selector) {
    let x = window.innerWidth  / 2;
    let y = window.innerHeight  / 2;
    let middler = $(document.elementFromPoint(x, y));
    let result = middler.closest(selector);
    console.log("closest:");
    console.log(result);
    return result;
}
scroller.containerSelector = '.post'

scroller.nextHandler = function () {
    let searched = scroller.currentContainerSeeker(scroller.containerSelector);
    if (searched.length == 0) {
        scroller.currentContainerNotFoundCallBack();
        return;
    }
    let nextElement = searched.next();
    var scrollTo = nextElement.length == 0 ? searched.offset().top + searched.height() : nextElement.offset().top;
    $('html, body').animate({
        scrollTop: scrollTo
    }, 400);
};

scroller.currentContainerNotFoundCallBack = function () {
    Materialize.toast("I found nothing :(", 500);
}

/**
 * @param  {jQuery} element
 */
scroller.attachNextClick = function (element) {
    element.click(scroller.nextHandler);
}

scroller.prevHandler = function () {
    let searched = scroller.currentContainerSeeker(scroller.containerSelector);
    if (searched.length == 0) {
        scroller.currentContainerNotFoundCallBack();
        return;
    }
    let prevElement = searched.prev();
    var scrollTo = prevElement.length == 0 ? searched.offset().top : prevElement.offset().top;
    $('html, body').animate({
        scrollTop: scrollTo
    }, 400);
};

scroller.attachPrevClick = function (element) {
    element.click(scroller.prevHandler)
}
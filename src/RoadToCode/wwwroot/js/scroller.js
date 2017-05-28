"use strict";

let scroller = {}
scroller.containerSelector = '.post'
scroller.currentContainerSeeker = function (selector) {
    let x = window.innerWidth  / 2;
    let y = window.innerHeight  / 2;
    let middler = $(document.elementFromPoint(x, y));
    let result = middler.closest(selector);
    return result;
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

/**
 * @param  {jQuery} element -Element to attach handler at
 */
scroller.attachNextClick = function (element) {
    element.click(scroller.nextHandler);
}

/**
 * @param  {jQuery} element -Element to attach handler at
 */
scroller.attachPrevClick = function (element) {
    element.click(scroller.prevHandler)
}

scroller.currentContainerNotFoundCallBack = function () {
    Materialize.toast("I found nothing :(", 500);
}

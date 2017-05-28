"use strict";

let Scroller = function () {
    this.containerSelector = '.post'
    let animateOptions = {
        duration: 400,
        queue: false,
    };

    /**
     * @param  {jQuery} element -Element to attach handler at
     */
    this.attachNextClick = function (element) {
        element.click(this.nextHandler);
    }
    /**
     * @param  {jQuery} element -Element to attach handler at
     */
    this.attachPrevClick = function (element) {
        element.click(this.prevHandler)
    }

    this.currentContainerSeeker = function (selector) {
        let x = window.innerWidth / 2;
        let y = window.innerHeight / 2;
        let middler = $(document.elementFromPoint(x, y));
        let result = middler.closest(selector);
        return result;
    }

    this.prevHandler = function () {
        let searched = this.currentContainerSeeker(this.containerSelector);
        if (searched.length == 0) {
            this.currentContainerNotFoundCallBack();
            return;
        }
        let prevElement = searched.prev();
        var scrollTo = prevElement.length == 0 ? searched.offset().top : prevElement.offset().top;
        $('html, body').animate({
            scrollTop: scrollTo
        }, animateOptions);
    };
    this.nextHandler = function () {
        let searched = this.currentContainerSeeker(this.containerSelector);
        if (searched.length == 0) {
            this.currentContainerNotFoundCallBack();
            return;
        }
        let nextElement = searched.next();
        var scrollTo = nextElement.length == 0 ? searched.offset().top + searched.height() : nextElement.offset().top;
        $('html, body').animate({
            scrollTop: scrollTo
        }, animateOptions);
    };

    this.currentContainerNotFoundCallBack = function () {
        Materialize.toast("I found nothing :(", 500);
    }
}
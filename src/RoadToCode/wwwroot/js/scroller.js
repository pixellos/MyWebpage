"use strict";

let Scroller = function () {
    let animateOptions = {
        duration: 400,
        queue: false,
    };

    this.animate = function(y){
        $('html, body').animate({
            scrollTop: y
        }, animateOptions);
    }
};
(function ScrollerInit() {
    Scroller.prototype.containerSelector = ".post";

    function currentContainerNotFoundCallBack() {
        Materialize.toast("I found nothing :(", 500);
    }

    function currentContainerSeeker(selector) {
        let x = window.innerWidth / 2;
        let y = window.innerHeight / 2;
        let middler = $(document.elementFromPoint(x, y));
        let result = middler.closest(selector);
        return result;
    }

    /**
     * @param  {jQuery} element -Element to attach handler at
     */
    Scroller.prototype.attachNextClick = function (element) {
        element.click(this.nextHandler);
    }

    /**
     * @param  {jQuery} element -Element to attach handler at
     */
    Scroller.prototype.attachPrevClick = function (element) {
        element.click(this.prevHandler)
    }

    Scroller.prototype.prevHandler = function () {
        let searched = currentContainerSeeker(this.containerSelector);
        if (searched.length == 0) {
            currentContainerNotFoundCallBack();
            return;
        }
        let prevElement = searched.prev();
        var scrollTo = prevElement.length == 0 ? searched.offset().top : prevElement.offset().top;
        this.animate(scrollTo);
    };

    Scroller.prototype.nextHandler = function () {
        let searched = currentContainerSeeker(this.containerSelector);
        if (searched.length == 0) {
            currentContainerNotFoundCallBack();
            return;
        }
        let nextElement = searched.next();
        var scrollTo = nextElement.length == 0 ? searched.offset().top + searched.height() : nextElement.offset().top;
        this.animate(scrollTo);
    };
})();
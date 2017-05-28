"use strict";
if (typeof (Scroller) != "function")
{
    console.error("scroller must have been referenced before config.")
}    
(function setupScroller() {
    let scrollerInstance = new Scroller();
    scrollerInstance.containerSelector = '.post'
    scrollerInstance.attachNextClick($("#nextButton"))
    scrollerInstance.attachPrevClick($("#prevButton"))

    window.addEventListener('keydown', function (event) {
        switch (event.keyCode) {
            case 37: // Left
                scrollerInstance.prevHandler();
                break;
            case 38: // Up
                scrollerInstance.prevHandler();
                break;
            case 39: // Right
                scrollerInstance.nextHandler();
                break;
            case 40: // Down
                scrollerInstance.nextHandler();
                break;
        }
    }, false);
})();
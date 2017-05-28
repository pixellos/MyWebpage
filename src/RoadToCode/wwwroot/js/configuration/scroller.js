"use strict";
if (typeof (scroller) != "object")
{
    console.error("scroller must have been referenced before config.")
}    
scroller.attachNextClick($("#nextButton"))
scroller.attachPrevClick($("#prevButton"))
window.addEventListener('keydown', function(event) {
  switch (event.keyCode) {
    case 37: // Left
          scroller.prevHandler();
    break;
    case 38: // Up
          scroller.prevHandler();
    break;
    case 39: // Right
        scroller.nextHandler();
    break;
    case 40: // Down
         scroller.nextHandler();
    break;
  }
}, false);
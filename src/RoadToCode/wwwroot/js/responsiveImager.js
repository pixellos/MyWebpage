"use strict";

var imager = {
    replacers: [{
        regex: /\|(I|i)mg:(http:\/\/imgur.com\/([^|]*))./g,
        getReplaceString: function () {
            let linkResult = "$2";
            let width = $(document).width();
            var sizeChar = "";
            switch (true) {
                case width < 200:
                    sizeChar = "t";
                    break;
                case width < 500:
                    sizeChar = "m";
                    break;
                case width < 800:
                    sizeChar = "l";
                    break;
                case width < 1400:
                    sizeChar = "h";
                    break;
                default:
                    break;
            }
            var result = `<img class="responsive-img" src="${linkResult}${sizeChar}.jpg" />`;
            return result;
        },
    }],
};

imager.replaceImages = function (element) {
    imager.replacers.forEach(function (item) {
        let replaceTo = item.getReplaceString();
        let replaced = $(element).html().replace(item.regex, replaceTo)
        $(element).html(replaced);
    });
};
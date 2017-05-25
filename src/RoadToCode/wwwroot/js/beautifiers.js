var beautifiers = {};
beautifiers.converter = new showdown.Converter();
beautifiers.flexTextToP = function (element) {
    element.find("p").addClass("flex-text truncate");
};
beautifiers.convertMarkdown = function (element) {
    element.find(".markdown").each(function () {
        let input = $(this).text();
        let result = beautifiers.converter.makeHtml(input);
        $(this).html(result);
    });
};
beautifiers.highlightCode = function (element) {
    element.find("pre code").each(function () {
        var result = hljs.highlightBlock($(this)[0]);
        $(this).parent().html(result);
    });
};
beautifiers.beautify = function (element) {
    for (const key of Object.keys(beautifiers)) {
        const beautifier = beautifiers[key];
        if (beautifier instanceof Function && key != "beautify") {
            beautifier(element);
        }
    }
    return element;
}
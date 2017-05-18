// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Markdig;
// using Microsoft.AspNetCore.Razor.TagHelpers;

// namespace RoadToCode.TagHelpers
// {
//     [HtmlTargetElement("markdown")]
//     public class MarkdownTagHelper : TagHelper
//     {
//         /// <summary>
//         /// This class is a response to my anger beacuse Markdown.ToHtml is failing to process string with spaces and \r\n
//         /// </summary>
//         private class Replacer
//         {
//             private Dictionary<string, string> Replacements { get; }
//             private Dictionary<string, string> UnReplacements { get; }
//             public Replacer(Dictionary<string, string> entries, Dictionary<string, string> unReplacementsOverrides)
//             {
//                 this.Replacements = entries;
//                 this.UnReplacements = unReplacementsOverrides;
//             }

//             public string Replace(string input)
//             {
//                 var result = input;
//                 foreach (var replacement in this.Replacements)
//                 {
//                     result = result.Replace(replacement.Key, replacement.Value);
//                 }
//                 return result;
//             }

//             public string UnReplace(string input)
//             {
//                 var result = input;
//                 foreach (var replacement in this.UnReplacements)
//                 {
//                     result = result.Replace(replacement.Key, replacement.Value);
//                 }
//                 foreach (var replacement in this.Replacements)
//                 {
//                     result = result.Replace(replacement.Value, replacement.Key);
//                 }
//                 return result;
//             }
//         }

//         public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//         {
//             var markdownReplacer = new Replacer(
//                 new Dictionary<string, string>(){
//                      { "    " , "!TabIndent;"},  
//                      { " " , "!Space;"},  
//                      }, new Dictionary<string, string>(){
//                      });
//             output.TagName = "p";
//             var children = await output.GetChildContentAsync();
//             var content = markdownReplacer.Replace(children.GetContent());
//             var markdown = Markdown.ToHtml(content);
//             var result = markdownReplacer.UnReplace(markdown);
//             output.Content.SetHtmlContent(result);
//         }
//     }
// }
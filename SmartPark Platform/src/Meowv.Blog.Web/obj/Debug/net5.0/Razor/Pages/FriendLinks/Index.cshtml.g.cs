#pragma checksum "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3fa27a38423ba8c6a170c92038d5d00001968ae9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_FriendLinks_Index), @"mvc.1.0.razor-page", @"/Pages/FriendLinks/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/friendlinks")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fa27a38423ba8c6a170c92038d5d00001968ae9", @"/Pages/FriendLinks/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c66e443ba81bfd444e2b1c1ae94c4deedf2b8d44", @"/Pages/_ViewImports.cshtml")]
    public class Pages_FriendLinks_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml"
  
    ViewData["Title"] = "FriendLinks - ";

    var response = Model.FriendLinks;
    var friendlinks = response.Result;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"post-wrap categories\">\r\n    <h2 class=\"post-title\">-&nbsp;FriendLinks&nbsp;-</h2>\r\n    <div class=\"categories-card\">\r\n");
#nullable restore
#line 12 "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml"
         foreach (var item in friendlinks)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card-item\">\r\n                <div class=\"categories\">\r\n                    <a target=\"_blank\"");
            BeginWriteAttribute("href", " href=\"", 507, "\"", 523, 1);
#nullable restore
#line 16 "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml"
WriteAttributeValue("", 514, item.Url, 514, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <h3>");
#nullable restore
#line 17 "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                    </a>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 21 "D:\Github\Blog\src\Meowv.Blog.Web\Pages\FriendLinks\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Meowv.Blog.Web.Pages.FriendLinks.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Meowv.Blog.Web.Pages.FriendLinks.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Meowv.Blog.Web.Pages.FriendLinks.IndexModel>)PageContext?.ViewData;
        public Meowv.Blog.Web.Pages.FriendLinks.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

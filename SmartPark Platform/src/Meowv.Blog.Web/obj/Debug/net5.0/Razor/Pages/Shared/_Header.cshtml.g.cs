#pragma checksum "D:\Github\Blog\src\Meowv.Blog.Web\Pages\Shared\_Header.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23b2d776354083616b41866dadd3c2ffb8285484"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Shared__Header), @"mvc.1.0.view", @"/Pages/Shared/_Header.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23b2d776354083616b41866dadd3c2ffb8285484", @"/Pages/Shared/_Header.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c66e443ba81bfd444e2b1c1ae94c4deedf2b8d44", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared__Header : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<header>
    <nav class=""navbar"">
        <div class=""container"">
            <div class=""navbar-header header-logo"">
                <a href=""/"">CqqTest</a>
            </div>
            <div class=""menu navbar-right"">
                <a class=""menu-item"" href=""/posts"">Posts</a>
                <a class=""menu-item"" href=""/categories"">Categories</a>
                <a class=""menu-item"" href=""/tags"">Tags</a>
                <a class=""menu-item apps"" href=""/apps"">Apps</a>
                <input id=""switch_default"" type=""checkbox"" class=""switch_default"">
                <label for=""switch_default"" class=""toggleBtn""></label>
            </div>
        </div>
    </nav>
    <nav class=""navbar-mobile"" id=""nav-mobile"">
        <div class=""container"">
            <div class=""navbar-header"">
                <div>
                    <a href=""/"">CqqTest</a>
                    <a id=""mobile-toggle-theme"">&nbsp;·&nbsp;Light</a>
                </div>
                <div class=""menu-toggle"">&#977");
            WriteLiteral(@"6; Menu</div>
            </div>
            <div class=""menu"" id=""mobile-menu"">
                <a class=""menu-item"" href=""/posts"">Posts</a>
                <a class=""menu-item"" href=""/categories"">Categories</a>
                <a class=""menu-item"" href=""/tags"">Tags</a>
                <a class=""menu-item apps"" href=""/apps"">Apps</a>
            </div>
        </div>
    </nav>
</header>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

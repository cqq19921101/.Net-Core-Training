#pragma checksum "D:\Github\Blog\src\Meowv.Blog.Admin\Shared\Footer.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f6c9896ac590a896c44047b42c78f98164415b9"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign.Pro.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Blog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Signatures;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Sayings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Github\Blog\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Messages;

#line default
#line hidden
#nullable disable
    public partial class Footer : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<AntDesign.Pro.Layout.FooterView>(0);
            __builder.AddAttribute(1, "Copyright", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 1 "D:\Github\Blog\src\Meowv.Blog.Admin\Shared\Footer.razor"
                         $"{DateTime.Now.Year} Cqq Test"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Links", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.Pro.Layout.LinkItem[]>(
#nullable restore
#line 1 "D:\Github\Blog\src\Meowv.Blog.Admin\Shared\Footer.razor"
                                                                  Links

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "D:\Github\Blog\src\Meowv.Blog.Admin\Shared\Footer.razor"
     
        LinkItem[] Links { get; set; } =
        {
            new LinkItem
            {

            }
        };
    

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591

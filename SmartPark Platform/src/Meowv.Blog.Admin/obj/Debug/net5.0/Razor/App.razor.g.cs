#pragma checksum "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "24cdbd68ff831f57a5f12b6c0baa4ea6f65e3f6d"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign.Pro.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Blog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Signatures;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Sayings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Messages;

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.CascadingAuthenticationState>(0);
            __builder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Routing.Router>(2);
                __builder2.AddAttribute(3, "AppAssembly", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Reflection.Assembly>(
#nullable restore
#line 2 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor"
                          typeof(Program).Assembly

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(4, "PreferExactMatches", 
#nullable restore
#line 2 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor"
                                                                         true

#line default
#line hidden
#nullable disable
                );
                __builder2.AddAttribute(5, "Found", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.RouteData>)((routeData) => (__builder3) => {
                    __builder3.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeRouteView>(6);
                    __builder3.AddAttribute(7, "RouteData", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.RouteData>(
#nullable restore
#line 4 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor"
                                            routeData

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(8, "DefaultLayout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Type>(
#nullable restore
#line 4 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor"
                                                                       typeof(MainLayout)

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(9, "Authorizing", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Spin>(10);
                        __builder4.AddAttribute(11, "size", "large");
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.AddAttribute(12, "NotFound", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<Microsoft.AspNetCore.Components.LayoutView>(13);
                    __builder3.AddAttribute(14, "Layout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Type>(
#nullable restore
#line 11 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\App.razor"
                                 typeof(MainLayout)

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(15, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Result>(16);
                        __builder4.AddAttribute(17, "Status", "404");
                        __builder4.AddAttribute(18, "Title", "Sorry, there\'s nothing at this address.");
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(19, "\r\n    ");
                __builder2.OpenComponent<AntDesign.AntContainer>(20);
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
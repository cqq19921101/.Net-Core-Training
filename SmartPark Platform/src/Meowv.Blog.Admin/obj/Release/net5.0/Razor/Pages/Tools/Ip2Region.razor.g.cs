#pragma checksum "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "808469aeec63b475f92e7c85f7ab21212f625a42"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Pages.Tools
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using AntDesign.Pro.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Admin.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Blog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Signatures;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Sayings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\_Imports.razor"
using Meowv.Blog.Dto.Messages;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/tools/ip2region")]
    public partial class Ip2Region : PageBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<AntDesign.Pro.Layout.PageContainer>(0);
            __builder.AddAttribute(1, "Breadcrumb", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<AntDesign.Breadcrumb>(2);
                __builder2.AddAttribute(3, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(4);
                    __builder3.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Icon>(6);
                        __builder4.AddAttribute(7, "Type", "home");
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(8, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(9);
                    __builder3.AddAttribute(10, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Icon>(11);
                        __builder4.AddAttribute(12, "Type", "node-index");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>IP查询</span>");
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(14, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<AntDesign.Card>(15);
                __builder2.AddAttribute(16, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Row>(17);
                    __builder3.AddAttribute(18, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Col>(19);
                        __builder4.AddAttribute(20, "Span", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                     12

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(21, "Offset", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                 6

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Search>(23);
                            __builder5.AddAttribute(24, "Placeholder", "输入IP地址");
                            __builder5.AddAttribute(25, "Size", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                        InputSize.Large

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(26, "EnterButton", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.Boolean, System.String>>(
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                                                        "查询"

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(27, "OnSearch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, 
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                                                                                           OnSearch

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(28, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                                                                             ip

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(29, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => ip = __value, ip))));
                            __builder5.AddAttribute(30, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => ip));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(31, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.Divider>(32);
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(33, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.Row>(34);
                    __builder3.AddAttribute(35, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Col>(36);
                        __builder4.AddAttribute(37, "Span", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 20 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                     12

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(38, "Offset", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 20 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                 6

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(39, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Descriptions>(40);
                            __builder5.AddAttribute(41, "Bordered", true);
                            __builder5.AddAttribute(42, "Layout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 21 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                    DescriptionsLayout.Vertical

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(43, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.OpenComponent<AntDesign.DescriptionsItem>(44);
                                __builder6.AddAttribute(45, "Title", "IP地址");
                                __builder6.AddAttribute(46, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(47, 
#nullable restore
#line 22 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                        returnIp

#line default
#line hidden
#nullable disable
                                    );
                                }
                                ));
                                __builder6.CloseComponent();
                                __builder6.AddMarkupContent(48, "\r\n                        ");
                                __builder6.OpenComponent<AntDesign.DescriptionsItem>(49);
                                __builder6.AddAttribute(50, "Title", "归属地");
                                __builder6.AddAttribute(51, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(52, 
#nullable restore
#line 23 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Ip2Region.razor"
                                                       region

#line default
#line hidden
#nullable disable
                                    );
                                }
                                ));
                                __builder6.CloseComponent();
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591

#pragma checksum "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68c78ec4a8e8014008e8d2ea4e869c942a5d854c"
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/tools/cdn")]
    public partial class Cdn : PageBase
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
                        __builder4.AddAttribute(12, "Type", "cloud");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>CDN刷新</span>");
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
                __builder2.AddAttribute(16, "CardTabs", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Tabs>(17);
                    __builder3.AddAttribute(18, "Size", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                             TabSize.Large

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(19, "DefaultActiveKey", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                               type

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(20, "ActiveKey", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                 type

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(21, "OnChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, 
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                 OnTabChange

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.TabPane>(23);
                        __builder4.AddAttribute(24, "Key", "1");
                        __builder4.AddAttribute(25, "Tab", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(26, "URL刷新");
                        }
                        ));
                        __builder4.AddAttribute(27, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.TextArea>(28);
                            __builder5.AddAttribute(29, "Placeholder", "输入需要刷新的URL (需要http://或https://)，一行一个");
                            __builder5.AddAttribute(30, "MinRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 18 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                  10

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(31, "MaxRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 18 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                               20

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(32, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 18 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                                                 urls1

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(33, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => urls1 = __value, urls1))));
                            __builder5.AddAttribute(34, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => urls1));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(35, "\r\n                            <br><br>\r\n                            ");
                            __builder5.OpenComponent<AntDesign.Button>(36);
                            __builder5.AddAttribute(37, "type", 
#nullable restore
#line 20 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                           ButtonType.Primary

#line default
#line hidden
#nullable disable
                            );
                            __builder5.AddAttribute(38, "OnClick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 20 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                        SubmitAsync

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(39, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(40, "提交并刷新");
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(41, "\r\n                    ");
                        __builder4.OpenComponent<AntDesign.TabPane>(42);
                        __builder4.AddAttribute(43, "Key", "2");
                        __builder4.AddAttribute(44, "Tab", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(45, "目录刷新");
                        }
                        ));
                        __builder4.AddAttribute(46, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.TextArea>(47);
                            __builder5.AddAttribute(48, "Placeholder", "输入需要刷新目录的URL (需要http://或https://)，一行一个");
                            __builder5.AddAttribute(49, "MinRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 26 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                    10

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(50, "MaxRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 26 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                                 20

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(51, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 26 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                                                   urls2

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(52, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => urls2 = __value, urls2))));
                            __builder5.AddAttribute(53, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => urls2));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(54, "\r\n                            <br><br>\r\n                            ");
                            __builder5.OpenComponent<AntDesign.Button>(55);
                            __builder5.AddAttribute(56, "type", 
#nullable restore
#line 28 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                           ButtonType.Primary

#line default
#line hidden
#nullable disable
                            );
                            __builder5.AddAttribute(57, "OnClick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                        SubmitAsync

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(58, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(59, "提交并刷新");
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(60, "\r\n                    ");
                        __builder4.OpenComponent<AntDesign.TabPane>(61);
                        __builder4.AddAttribute(62, "Key", "3");
                        __builder4.AddAttribute(63, "Tab", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(64, "URL预热");
                        }
                        ));
                        __builder4.AddAttribute(65, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.TextArea>(66);
                            __builder5.AddAttribute(67, "Placeholder", "输入需要预热的URL (需要http://或https://)，一行一个");
                            __builder5.AddAttribute(68, "MinRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 34 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                  10

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(69, "MaxRows", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.UInt32>(
#nullable restore
#line 34 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                               20

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(70, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 34 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                                                                                 urls3

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(71, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => urls3 = __value, urls3))));
                            __builder5.AddAttribute(72, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => urls3));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(73, "\r\n                            <br><br>\r\n                            ");
                            __builder5.OpenComponent<AntDesign.Button>(74);
                            __builder5.AddAttribute(75, "type", 
#nullable restore
#line 36 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                           ButtonType.Primary

#line default
#line hidden
#nullable disable
                            );
                            __builder5.AddAttribute(76, "OnClick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 36 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Tools\Cdn.razor"
                                                                        SubmitAsync

#line default
#line hidden
#nullable disable
                            )));
                            __builder5.AddAttribute(77, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(78, "提交并预热");
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

#pragma checksum "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee2fa7975ef983f671b61d793055939eba2e0f03"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Pages.Signatures
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/signatures")]
    public partial class SignatureList : PageBase
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
                        __builder4.AddAttribute(12, "Type", "thunderbolt");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>个性签名</span>");
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
                    __builder3.OpenComponent<AntDesign.Table<SignatureDto>>(17);
                    __builder3.AddAttribute(18, "DataSource", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<SignatureDto>>(
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                signatures

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(19, "Bordered", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                             true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(20, "Size", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.TableSize>(
#nullable restore
#line 16 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                         TableSize.Small

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(21, "Total", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 17 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                          total

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(22, "OnPageIndexChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<AntDesign.PaginationEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<AntDesign.PaginationEventArgs>(this, 
#nullable restore
#line 20 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      HandlePageIndexChange

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.AddAttribute(23, "PageIndex", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 18 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                    page

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(24, "PageIndexChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Int32>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Int32>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => page = __value, page))));
                    __builder3.AddAttribute(25, "PageSize", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 19 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                   limit

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(26, "PageSizeChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Int32>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Int32>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => limit = __value, limit))));
                    __builder3.AddAttribute(27, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<SignatureDto>)((context) => (__builder4) => {
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_0(__builder4, 28, 29, "Id", 30, 
#nullable restore
#line 21 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                       false

#line default
#line hidden
#nullable disable
                        , 31, 
#nullable restore
#line 21 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.Id

#line default
#line hidden
#nullable disable
                        , 32, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Id = __value, context.Id)), 33, () => context.Id);
                        __builder4.AddMarkupContent(34, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_1(__builder4, 35, 36, "姓名", 37, 
#nullable restore
#line 22 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                         false

#line default
#line hidden
#nullable disable
                        , 38, 
#nullable restore
#line 22 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.Name

#line default
#line hidden
#nullable disable
                        , 39, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Name = __value, context.Name)), 40, () => context.Name);
                        __builder4.AddMarkupContent(41, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_2(__builder4, 42, 43, "类型", 44, 
#nullable restore
#line 23 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                         false

#line default
#line hidden
#nullable disable
                        , 45, 
#nullable restore
#line 23 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.Type

#line default
#line hidden
#nullable disable
                        , 46, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Type = __value, context.Type)), 47, () => context.Type);
                        __builder4.AddMarkupContent(48, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_3(__builder4, 49, 50, "链接", 51, 
#nullable restore
#line 24 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                        false

#line default
#line hidden
#nullable disable
                        , 52, 
#nullable restore
#line 24 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.Url

#line default
#line hidden
#nullable disable
                        , 53, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Url = __value, context.Url)), 54, () => context.Url, 55, (__builder5) => {
                            __builder5.OpenElement(56, "a");
                            __builder5.AddAttribute(57, "target", "_blank");
                            __builder5.AddAttribute(58, "href", "https://static.meowv.com/signature/" + (
#nullable restore
#line 25 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                                 context.Url

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddContent(59, 
#nullable restore
#line 25 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                                               context.Url

#line default
#line hidden
#nullable disable
                            );
                            __builder5.CloseElement();
                        }
                        );
                        __builder4.AddMarkupContent(60, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_4(__builder4, 61, 62, "IP地址", 63, 
#nullable restore
#line 27 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                         false

#line default
#line hidden
#nullable disable
                        , 64, 
#nullable restore
#line 27 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.Ip

#line default
#line hidden
#nullable disable
                        , 65, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Ip = __value, context.Ip)), 66, () => context.Ip);
                        __builder4.AddMarkupContent(67, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList.TypeInference.CreateColumn_5(__builder4, 68, 69, "创建时间", 70, 
#nullable restore
#line 28 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                                                false

#line default
#line hidden
#nullable disable
                        , 71, 
#nullable restore
#line 28 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                      context.CreatedAt

#line default
#line hidden
#nullable disable
                        , 72, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.CreatedAt = __value, context.CreatedAt)), 73, () => context.CreatedAt);
                        __builder4.AddMarkupContent(74, "\r\n                ");
                        __builder4.OpenComponent<AntDesign.ActionColumn>(75);
                        __builder4.AddAttribute(76, "Title", "操作");
                        __builder4.AddAttribute(77, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Space>(78);
                            __builder5.AddAttribute(79, "Size", "middle");
                            __builder5.AddAttribute(80, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.OpenComponent<AntDesign.SpaceItem>(81);
                                __builder6.AddAttribute(82, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.OpenComponent<AntDesign.Popconfirm>(83);
                                    __builder7.AddAttribute(84, "Title", "确定删除吗?");
                                    __builder7.AddAttribute(85, "Icon", "close-circle");
                                    __builder7.AddAttribute(86, "OnConfirm", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 34 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Signatures\SignatureList.razor"
                                                   (async () => await DeleteAsync(context.Id))

#line default
#line hidden
#nullable disable
                                    )));
                                    __builder7.AddAttribute(87, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder8) => {
                                        __builder8.OpenComponent<AntDesign.Button>(88);
                                        __builder8.AddAttribute(89, "Danger", true);
                                        __builder8.AddAttribute(90, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder9) => {
                                            __builder9.AddMarkupContent(91, "删除");
                                        }
                                        ));
                                        __builder8.CloseComponent();
                                    }
                                    ));
                                    __builder7.CloseComponent();
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
namespace __Blazor.Meowv.Blog.Admin.Pages.Signatures.SignatureList
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateColumn_0<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.CloseComponent();
        }
        public static void CreateColumn_1<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.CloseComponent();
        }
        public static void CreateColumn_2<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.CloseComponent();
        }
        public static void CreateColumn_3<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4, int __seq5, global::Microsoft.AspNetCore.Components.RenderFragment __arg5)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.AddAttribute(__seq5, "ChildContent", __arg5);
        __builder.CloseComponent();
        }
        public static void CreateColumn_4<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.CloseComponent();
        }
        public static void CreateColumn_5<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, TData __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg3, int __seq4, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "Field", __arg2);
        __builder.AddAttribute(__seq3, "FieldChanged", __arg3);
        __builder.AddAttribute(__seq4, "FieldExpression", __arg4);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591

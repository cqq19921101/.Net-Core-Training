#pragma checksum "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6d9dbfd82dd16d80e37476e5260d1a492950a2fc"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Pages.Categories
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/categories/add")]
    public partial class CategoryAdd : PageBase
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
                        __builder4.AddAttribute(12, "Type", "switcher");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>分类管理</span>");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(14, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(15);
                    __builder3.AddAttribute(16, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Icon>(17);
                        __builder4.AddAttribute(18, "Type", "plus-square");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(19, "<span>新增分类</span>");
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(20, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<AntDesign.Card>(21);
                __builder2.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __Blazor.Meowv.Blog.Admin.Pages.Categories.CategoryAdd.TypeInference.CreateForm_0(__builder3, 23, 24, 
#nullable restore
#line 14 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                          input

#line default
#line hidden
#nullable disable
                    , 25, 
#nullable restore
#line 15 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                8

#line default
#line hidden
#nullable disable
                    , 26, 
#nullable restore
#line 16 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                  8

#line default
#line hidden
#nullable disable
                    , 27, Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 17 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                            HandleSubmit

#line default
#line hidden
#nullable disable
                    ), 28, (context) => (__builder4) => {
                        __builder4.OpenComponent<AntDesign.FormItem>(29);
                        __builder4.AddAttribute(30, "Label", "分类名称");
                        __builder4.AddAttribute(31, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __Blazor.Meowv.Blog.Admin.Pages.Categories.CategoryAdd.TypeInference.CreateInput_1(__builder5, 32, 33, 
#nullable restore
#line 19 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                         context.Name

#line default
#line hidden
#nullable disable
                            , 34, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Name = __value, context.Name)), 35, () => context.Name);
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(36, "\r\n                ");
                        __builder4.OpenComponent<AntDesign.FormItem>(37);
                        __builder4.AddAttribute(38, "Label", "分类别名");
                        __builder4.AddAttribute(39, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __Blazor.Meowv.Blog.Admin.Pages.Categories.CategoryAdd.TypeInference.CreateInput_2(__builder5, 40, 41, 
#nullable restore
#line 22 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                         context.Alias

#line default
#line hidden
#nullable disable
                            , 42, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Alias = __value, context.Alias)), 43, () => context.Alias);
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(44, "\r\n                ");
                        __builder4.OpenComponent<AntDesign.FormItem>(45);
                        __builder4.AddAttribute(46, "WrapperColOffset", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 24 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                            8

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(47, "WrapperColSpan", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 24 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                                               8

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(48, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Button>(49);
                            __builder5.AddAttribute(50, "Type", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 25 "D:\WorkShop\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Categories\CategoryAdd.razor"
                                   ButtonType.Primary

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(51, "HtmlType", "submit");
                            __builder5.AddAttribute(52, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(53, "保存");
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    );
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
namespace __Blazor.Meowv.Blog.Admin.Pages.Categories.CategoryAdd
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateForm_0<TModel>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TModel __arg0, int __seq1, global::OneOf.OneOf<global::System.String, global::System.Int32> __arg1, int __seq2, global::OneOf.OneOf<global::System.String, global::System.Int32> __arg2, int __seq3, global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Forms.EditContext> __arg3, int __seq4, global::Microsoft.AspNetCore.Components.RenderFragment<TModel> __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Form<TModel>>(seq);
        __builder.AddAttribute(__seq0, "Model", __arg0);
        __builder.AddAttribute(__seq1, "LabelColSpan", __arg1);
        __builder.AddAttribute(__seq2, "WrapperColSpan", __arg2);
        __builder.AddAttribute(__seq3, "OnFinish", __arg3);
        __builder.AddAttribute(__seq4, "ChildContent", __arg4);
        __builder.CloseComponent();
        }
        public static void CreateInput_1<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TValue __arg0, int __seq1, global::Microsoft.AspNetCore.Components.EventCallback<TValue> __arg1, int __seq2, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg2)
        {
        __builder.OpenComponent<global::AntDesign.Input<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Value", __arg0);
        __builder.AddAttribute(__seq1, "ValueChanged", __arg1);
        __builder.AddAttribute(__seq2, "ValueExpression", __arg2);
        __builder.CloseComponent();
        }
        public static void CreateInput_2<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TValue __arg0, int __seq1, global::Microsoft.AspNetCore.Components.EventCallback<TValue> __arg1, int __seq2, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg2)
        {
        __builder.OpenComponent<global::AntDesign.Input<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Value", __arg0);
        __builder.AddAttribute(__seq1, "ValueChanged", __arg1);
        __builder.AddAttribute(__seq2, "ValueExpression", __arg2);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591

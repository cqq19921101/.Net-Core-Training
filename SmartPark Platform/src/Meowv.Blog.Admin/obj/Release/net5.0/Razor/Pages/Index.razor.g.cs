#pragma checksum "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f33cb7ba18699bb92a27f2598384f222d8646b4"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Pages
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : PageBase
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
                        __builder4.AddAttribute(12, "Type", "dashboard");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>Dashboard</span>");
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
                __builder2.AddAttribute(16, "Bordered", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 12 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                        false

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Row>(18);
                    __builder3.AddAttribute(19, "Gutter", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.Int32, System.Collections.Generic.Dictionary<System.String, System.Int32>, (System.Int32, System.Int32)>>(
#nullable restore
#line 13 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                         16

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(20, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Col>(21);
                        __builder4.AddAttribute(22, "Span", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 14 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                     8

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(23, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Card>(24);
                            __builder5.AddAttribute(25, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateStatistic_0(__builder6, 26, 27, "文章", 28, 
#nullable restore
#line 16 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                      statistics.Item1

#line default
#line hidden
#nullable disable
                                , 29, "篇", 30, (__builder7) => {
                                    __builder7.OpenElement(31, "span");
                                    __builder7.OpenComponent<AntDesign.Icon>(32);
                                    __builder7.AddAttribute(33, "Type", "read");
                                    __builder7.CloseComponent();
                                    __builder7.CloseElement();
                                }
                                );
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(34, "\r\n                ");
                        __builder4.OpenComponent<AntDesign.Col>(35);
                        __builder4.AddAttribute(36, "Span", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 23 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                     8

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(37, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Card>(38);
                            __builder5.AddAttribute(39, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateStatistic_1(__builder6, 40, 41, "分类", 42, 
#nullable restore
#line 25 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                      statistics.Item2

#line default
#line hidden
#nullable disable
                                , 43, "个", 44, (__builder7) => {
                                    __builder7.OpenElement(45, "span");
                                    __builder7.OpenComponent<AntDesign.Icon>(46);
                                    __builder7.AddAttribute(47, "Type", "switcher");
                                    __builder7.CloseComponent();
                                    __builder7.CloseElement();
                                }
                                );
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(48, "\r\n                ");
                        __builder4.OpenComponent<AntDesign.Col>(49);
                        __builder4.AddAttribute(50, "Span", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<System.String, System.Int32>>(
#nullable restore
#line 32 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                     8

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(51, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.OpenComponent<AntDesign.Card>(52);
                            __builder5.AddAttribute(53, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateStatistic_2(__builder6, 54, 55, "标签", 56, 
#nullable restore
#line 34 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                      statistics.Item3

#line default
#line hidden
#nullable disable
                                , 57, "个", 58, (__builder7) => {
                                    __builder7.OpenElement(59, "span");
                                    __builder7.OpenComponent<AntDesign.Icon>(60);
                                    __builder7.AddAttribute(61, "Type", "tags");
                                    __builder7.CloseComponent();
                                    __builder7.CloseElement();
                                }
                                );
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
                __builder2.AddMarkupContent(62, "\r\n        ");
                __builder2.OpenComponent<AntDesign.Card>(63);
                __builder2.AddAttribute(64, "Bordered", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 43 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                        false

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(65, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Table<NameValue>>(66);
                    __builder3.AddAttribute(67, "DataSource", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<NameValue>>(
#nullable restore
#line 44 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                  data

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(68, "Loading", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 44 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                 isLoading

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(69, "Bordered", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 44 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                                      true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(70, "HidePagination", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 44 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                                                           true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(71, "Size", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.TableSize>(
#nullable restore
#line 44 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                                                                       TableSize.Small

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(72, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<NameValue>)((context) => (__builder4) => {
                        __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateColumn_3(__builder4, 73, 74, "服务名称", 75, 
#nullable restore
#line 45 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                           false

#line default
#line hidden
#nullable disable
                        , 76, 
#nullable restore
#line 45 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                      context.Name

#line default
#line hidden
#nullable disable
                        , 77, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Name = __value, context.Name)), 78, () => context.Name, 79, (__builder5) => {
                            __builder5.OpenComponent<AntDesign.Text>(80);
                            __builder5.AddAttribute(81, "Strong", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 46 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                  true

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(82, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddContent(83, 
#nullable restore
#line 46 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                         context.Name

#line default
#line hidden
#nullable disable
                                );
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        );
                        __builder4.AddMarkupContent(84, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateColumn_4(__builder4, 85, 86, "健康状态", 87, 
#nullable restore
#line 48 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                            false

#line default
#line hidden
#nullable disable
                        , 88, 
#nullable restore
#line 48 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                                                  2

#line default
#line hidden
#nullable disable
                        , 89, 
#nullable restore
#line 48 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                      context.Value

#line default
#line hidden
#nullable disable
                        , 90, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Value = __value, context.Value)), 91, () => context.Value);
                        __builder4.AddMarkupContent(92, "\r\n                ");
                        __Blazor.Meowv.Blog.Admin.Pages.Index.TypeInference.CreateColumn_5(__builder4, 93, 94, 
#nullable restore
#line 49 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                                                    0

#line default
#line hidden
#nullable disable
                        , 95, 
#nullable restore
#line 49 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                      context.Value

#line default
#line hidden
#nullable disable
                        , 96, Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => context.Value = __value, context.Value)), 97, () => context.Value, 98, (__builder5) => {
                            __builder5.OpenComponent<AntDesign.Badge>(99);
                            __builder5.AddAttribute(100, "Status", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 50 "D:\Github\Blog\src\Meowv.Blog.Admin\Pages\Index.razor"
                                     context.Value == "Healthy" ? "processing" : "error"

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.CloseComponent();
                        }
                        );
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
namespace __Blazor.Meowv.Blog.Admin.Pages.Index
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateStatistic_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, TValue __arg1, int __seq2, global::System.String __arg2, int __seq3, global::Microsoft.AspNetCore.Components.RenderFragment __arg3)
        {
        __builder.OpenComponent<global::AntDesign.Statistic<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Value", __arg1);
        __builder.AddAttribute(__seq2, "Suffix", __arg2);
        __builder.AddAttribute(__seq3, "PrefixTemplate", __arg3);
        __builder.CloseComponent();
        }
        public static void CreateStatistic_1<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, TValue __arg1, int __seq2, global::System.String __arg2, int __seq3, global::Microsoft.AspNetCore.Components.RenderFragment __arg3)
        {
        __builder.OpenComponent<global::AntDesign.Statistic<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Value", __arg1);
        __builder.AddAttribute(__seq2, "Suffix", __arg2);
        __builder.AddAttribute(__seq3, "PrefixTemplate", __arg3);
        __builder.CloseComponent();
        }
        public static void CreateStatistic_2<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, TValue __arg1, int __seq2, global::System.String __arg2, int __seq3, global::Microsoft.AspNetCore.Components.RenderFragment __arg3)
        {
        __builder.OpenComponent<global::AntDesign.Statistic<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Value", __arg1);
        __builder.AddAttribute(__seq2, "Suffix", __arg2);
        __builder.AddAttribute(__seq3, "PrefixTemplate", __arg3);
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
        public static void CreateColumn_4<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.String __arg0, int __seq1, global::System.Boolean __arg1, int __seq2, global::System.Int32 __arg2, int __seq3, TData __arg3, int __seq4, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg4, int __seq5, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg5)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "Title", __arg0);
        __builder.AddAttribute(__seq1, "Sortable", __arg1);
        __builder.AddAttribute(__seq2, "HeaderColSpan", __arg2);
        __builder.AddAttribute(__seq3, "Field", __arg3);
        __builder.AddAttribute(__seq4, "FieldChanged", __arg4);
        __builder.AddAttribute(__seq5, "FieldExpression", __arg5);
        __builder.CloseComponent();
        }
        public static void CreateColumn_5<TData>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Int32 __arg0, int __seq1, TData __arg1, int __seq2, global::Microsoft.AspNetCore.Components.EventCallback<TData> __arg2, int __seq3, global::System.Linq.Expressions.Expression<global::System.Func<TData>> __arg3, int __seq4, global::Microsoft.AspNetCore.Components.RenderFragment __arg4)
        {
        __builder.OpenComponent<global::AntDesign.Column<TData>>(seq);
        __builder.AddAttribute(__seq0, "HeaderColSpan", __arg0);
        __builder.AddAttribute(__seq1, "Field", __arg1);
        __builder.AddAttribute(__seq2, "FieldChanged", __arg2);
        __builder.AddAttribute(__seq3, "FieldExpression", __arg3);
        __builder.AddAttribute(__seq4, "ChildContent", __arg4);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591

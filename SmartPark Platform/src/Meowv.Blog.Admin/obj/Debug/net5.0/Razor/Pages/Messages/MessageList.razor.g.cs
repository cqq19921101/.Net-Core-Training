#pragma checksum "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b92598ce42bc39b95ceca3ac6af2740f759b2f54"
// <auto-generated/>
#pragma warning disable 1591
namespace Meowv.Blog.Admin.Pages.Messages
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/messages")]
    public partial class MessageList : PageBase
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
                        __builder4.AddAttribute(12, "Type", "message");
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(13, "<span>留言板</span>");
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
#line 12 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                        false

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Comment>(18);
                    __builder3.AddAttribute(19, "Avatar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 13 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                              MessageModel.Avatar

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(20, "ContentTemplate", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<Vditor.Editor>(21);
                        __builder4.AddAttribute(22, "Placeholder", "在这里输入你的留言内容...");
                        __builder4.AddAttribute(23, "Mode", "ir");
                        __builder4.AddAttribute(24, "Toolbar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Vditor.Models.Toolbar>(
#nullable restore
#line 18 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                            Toolbar

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(25, "Width", "100%");
                        __builder4.AddAttribute(26, "MinHeight", "300");
                        __builder4.AddAttribute(27, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 17 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                 MessageModel.Content

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(28, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => MessageModel.Content = __value, MessageModel.Content))));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(29, "\r\n                    <br>\r\n                    ");
                        __builder4.OpenComponent<AntDesign.Button>(30);
                        __builder4.AddAttribute(31, "type", 
#nullable restore
#line 22 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                   ButtonType.Primary

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddAttribute(32, "OnClick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 22 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                SubmitMessageAsync

#line default
#line hidden
#nullable disable
                        )));
                        __builder4.AddAttribute(33, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(34, "我要留言");
                        }
                        ));
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(35, "\r\n        ");
                __builder2.OpenComponent<AntDesign.Card>(36);
                __builder2.AddAttribute(37, "Bordered", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 26 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                        false

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(38, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
#nullable restore
#line 27 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
             foreach (var item in messages)
            {

#line default
#line hidden
#nullable disable
                    __builder3.OpenComponent<AntDesign.Comment>(39);
                    __builder3.AddAttribute(40, "Author", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 29 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                  item.Name

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(41, "Avatar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 30 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                  item.Avatar

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(42, "Datetime", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 31 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                    item.CreatedAt

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(43, "ContentTemplate", new Microsoft.AspNetCore.Components.RenderFragment(
#nullable restore
#line 32 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                           RenderContent(item.Content)

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(44, "Actions", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IList<Microsoft.AspNetCore.Components.RenderFragment>>(
#nullable restore
#line 33 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                    new[] { replyAction(item.Id), item.UserId == UserId ? deleteAction(item.Id) : null }

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(45, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
#nullable restore
#line 34 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                     if (item.Reply.Any())
                    {

#line default
#line hidden
#nullable disable
                        __builder4.OpenComponent<AntDesign.AntList<MessageReplyDto>>(46);
                        __builder4.AddAttribute(47, "DataSource", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<MessageReplyDto>>(
#nullable restore
#line 36 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                             item.Reply

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(48, "Size", "small");
                        __builder4.AddAttribute(49, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<MessageReplyDto>)((context) => (__builder5) => {
                            __builder5.OpenComponent<AntDesign.ListItem>(50);
                            __builder5.AddAttribute(51, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.OpenComponent<AntDesign.Comment>(52);
                                __builder6.AddAttribute(53, "Author", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 38 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                  context.Name

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.AddAttribute(54, "Avatar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 39 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                  context.Avatar

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.AddAttribute(55, "Datetime", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 40 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                    context.CreatedAt

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.AddAttribute(56, "ContentTemplate", new Microsoft.AspNetCore.Components.RenderFragment(
#nullable restore
#line 41 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                           RenderContent(context.Content)

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.AddAttribute(57, "Actions", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IList<Microsoft.AspNetCore.Components.RenderFragment>>(
#nullable restore
#line 42 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                    new[] { context.UserId == UserId ? deleteReplyAction(item.Id, context.Id) : null }

#line default
#line hidden
#nullable disable
                                ));
                                __builder6.CloseComponent();
                            }
                            ));
                            __builder5.CloseComponent();
                        }
                        ));
                        __builder4.CloseComponent();
#nullable restore
#line 46 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                    }

#line default
#line hidden
#nullable disable
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(58, "\r\n                ");
                    __builder3.OpenComponent<AntDesign.Divider>(59);
                    __builder3.AddAttribute(60, "Dashed", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 48 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                 true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
#nullable restore
#line 49 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
             if (total > limit)
            {

#line default
#line hidden
#nullable disable
                    __builder3.OpenElement(61, "div");
                    __builder3.AddAttribute(62, "style", "text-align:center");
                    __builder3.OpenComponent<AntDesign.Pagination>(63);
                    __builder3.AddAttribute(64, "Total", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 53 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                        total

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(65, "PageSize", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 53 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                          limit

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(66, "OnPageIndexChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<AntDesign.PaginationEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<AntDesign.PaginationEventArgs>(this, 
#nullable restore
#line 53 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                    HandlePageIndexChange

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.CloseComponent();
                    __builder3.CloseElement();
#nullable restore
#line 55 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
            }

#line default
#line hidden
#nullable disable
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(67, "\r\n        ");
                __builder2.OpenComponent<AntDesign.Drawer>(68);
                __builder2.AddAttribute(69, "Closable", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 57 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                          true

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(70, "Visible", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 58 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                         visible

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(71, "Placement", "right");
                __builder2.AddAttribute(72, "Title", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<OneOf.OneOf<Microsoft.AspNetCore.Components.RenderFragment, System.String>>(
#nullable restore
#line 60 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                       ("回复留言")

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(73, "Width", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 61 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                       1000

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(74, "OnClose", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, 
#nullable restore
#line 62 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                         _ => Close()

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(75, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.Comment>(76);
                    __builder3.AddAttribute(77, "Avatar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 63 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                              ReplyMessageModel.Avatar

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(78, "ContentTemplate", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<Vditor.Editor>(79);
                        __builder4.AddAttribute(80, "Placeholder", "在这里输入你的留言内容...");
                        __builder4.AddAttribute(81, "Mode", "ir");
                        __builder4.AddAttribute(82, "Toolbar", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Vditor.Models.Toolbar>(
#nullable restore
#line 68 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                            Toolbar

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(83, "Width", "100%");
                        __builder4.AddAttribute(84, "MinHeight", "300");
                        __builder4.AddAttribute(85, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 67 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                 ReplyMessageModel.Content

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(86, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => ReplyMessageModel.Content = __value, ReplyMessageModel.Content))));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(87, "\r\n                    <br>\r\n                    ");
                        __builder4.OpenComponent<AntDesign.Button>(88);
                        __builder4.AddAttribute(89, "type", 
#nullable restore
#line 72 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                   ButtonType.Primary

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddAttribute(90, "OnClick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 72 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                SubmitReplyMessageAsync

#line default
#line hidden
#nullable disable
                        )));
                        __builder4.AddAttribute(91, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(92, "回复");
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
#nullable restore
#line 79 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
      
    RenderFragment replyAction(string id) =>

#line default
#line hidden
#nullable disable
        (__builder2) => {
            __builder2.OpenComponent<AntDesign.Button>(93);
            __builder2.AddAttribute(94, "Type", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 80 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                            ButtonType.Link

#line default
#line hidden
#nullable disable
            ));
            __builder2.AddAttribute(95, "Size", "small");
            __builder2.AddAttribute(96, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 80 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                    _ => Open(id)

#line default
#line hidden
#nullable disable
            ));
            __builder2.AddAttribute(97, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                __builder3.AddMarkupContent(98, "回复");
            }
            ));
            __builder2.CloseComponent();
        }
#nullable restore
#line 80 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                              ;

    RenderFragment deleteAction(string id) => 

#line default
#line hidden
#nullable disable
        (__builder2) => {
            __builder2.OpenComponent<AntDesign.Popconfirm>(99);
            __builder2.AddAttribute(100, "Title", "确定删除吗?");
            __builder2.AddAttribute(101, "Icon", "close-circle");
            __builder2.AddAttribute(102, "OnConfirm", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 82 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                         (async () => await DeleteMessageAsync(id))

#line default
#line hidden
#nullable disable
            )));
            __builder2.AddAttribute(103, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                __builder3.OpenComponent<AntDesign.Button>(104);
                __builder3.AddAttribute(105, "Type", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 82 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                                                                    ButtonType.Link

#line default
#line hidden
#nullable disable
                ));
                __builder3.AddAttribute(106, "Danger", true);
                __builder3.AddAttribute(107, "Size", "small");
                __builder3.AddAttribute(108, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                    __builder4.AddMarkupContent(109, "删除");
                }
                ));
                __builder3.CloseComponent();
            }
            ));
            __builder2.CloseComponent();
        }
#nullable restore
#line 82 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                                                                                                                                 ;

    RenderFragment deleteReplyAction(string id, string replyId) => 

#line default
#line hidden
#nullable disable
        (__builder2) => {
            __builder2.OpenComponent<AntDesign.Popconfirm>(110);
            __builder2.AddAttribute(111, "Title", "确定删除吗?");
            __builder2.AddAttribute(112, "Icon", "close-circle");
            __builder2.AddAttribute(113, "OnConfirm", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 84 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                              (async () => await DeleteReplyMessageAsync(id, replyId))

#line default
#line hidden
#nullable disable
            )));
            __builder2.AddAttribute(114, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                __builder3.OpenComponent<AntDesign.Button>(115);
                __builder3.AddAttribute(116, "Type", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 84 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                                                                                                       ButtonType.Link

#line default
#line hidden
#nullable disable
                ));
                __builder3.AddAttribute(117, "Danger", true);
                __builder3.AddAttribute(118, "Size", "small");
                __builder3.AddAttribute(119, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                    __builder4.AddMarkupContent(120, "删除");
                }
                ));
                __builder3.CloseComponent();
            }
            ));
            __builder2.CloseComponent();
        }
#nullable restore
#line 84 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                                                                                                                                                                    ;

    RenderFragment RenderContent2(string content) => 

#line default
#line hidden
#nullable disable
        (__builder2) => {
            __builder2.OpenElement(121, "div");
            __builder2.AddAttribute(122, "style", "margin-top:15px;");
            __builder2.AddContent(123, 
#nullable restore
#line 86 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                      (MarkupString)@content

#line default
#line hidden
#nullable disable
            );
            __builder2.CloseElement();
        }
#nullable restore
#line 86 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                                   ;

    RenderFragment RenderContent(string content) => 

#line default
#line hidden
#nullable disable
        (__builder2) => {
            __builder2.OpenComponent<Vditor.Preview>(124);
            __builder2.AddAttribute(125, "Markdown", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 88 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                content

#line default
#line hidden
#nullable disable
            ));
            __builder2.CloseComponent();
        }
#nullable restore
#line 88 "D:\Github\.Net-Core-Training\SmartPark Platform\src\Meowv.Blog.Admin\Pages\Messages\MessageList.razor"
                                                                                                          ;

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
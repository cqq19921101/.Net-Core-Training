using AntDesign;
using Meowv.Blog.Admin.Services;
using Meowv.Blog.Dto.Authorize.Params;
using Meowv.Blog.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meowv.Blog.Admin.Pages.OAuth
{
    public partial class Login
    {
        private readonly LoginInput model = new LoginInput();

        [Inject] public NotificationService Notification { get; set; }

        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        bool loading = false;

        public async Task HandleSubmit()
        {
            var desc = "The username or password entered is incorrect.";

            if (model.Type == "code")
            {
                desc = "The code entered is incorrect.";

                if (string.IsNullOrWhiteSpace(model.Code))
                {
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return;
                }
            }

            var service = AuthenticationStateProvider as OAuthService;

            var token = await service.GetTokenAsync(model);

            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJjcXEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiIxMjNAbWVvd3YuY29tIiwiYXZhdGFyIjoiaHR0cDovL3ExLnFsb2dvLmNuL2c_Yj1xcSZuaz00OTQ5MTA4ODcmcz02NDAiLCJleHAiOjE2MTUzODU5OTMsIm5iZiI6IjE2MTUzNDI3ODgiLCJpc3MiOiJtZW93dl9ibG9nIiwiYXVkIjoibWVvd3ZfYmxvZyBhcGkifQ.BKP3itidQe4Bjx4OaC0vijVrU5nE9nCUMGLkBwsoC0I";

            if (string.IsNullOrEmpty(token))
            {
                await Notification.Warning(new NotificationConfig
                {
                    Message = "UnAuthorized",
                    Description = desc
                });
            }
            else
            {
                await Notification.Success(new NotificationConfig
                {
                    Message = "Successful",
                    Description = $"Login is successful, welcome back.",
                    Duration = 0.5
                });
                //NavigationManager.NavigateTo("/categories/list", true);
                NavigationManager.NavigateTo("/", true);
            }
        }

        public void HandleClick(string type)
        {
            NavigationManager.NavigateTo($"/oauth/{type}");
        }

        public void OnChange(string activeKey)
        {
            model.Type = activeKey;
        }

        public async Task GetAuthCode()
        {
            loading = true;

            var response = await GetResultAsync<BlogResponse>("api/meowv/oauth/code/send", method: HttpMethod.Post);
            if (response.Success)
            {
                await Notification.Success(new NotificationConfig
                {
                    Message = "Successful",
                    Description = "The dynamic code has been sent to WeChat."
                });
            }
            else
            {
                await Message.Error(response.Message);
            }

            await Task.Run(async () =>
            {
                await Task.Delay(8000);
                loading = false;
                await InvokeAsync(StateHasChanged);
            });
        }
    }
}
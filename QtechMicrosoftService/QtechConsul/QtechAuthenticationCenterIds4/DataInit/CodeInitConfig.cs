using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QtechAuthenticationCenterIds4.DataInit
{
    /// <summary>
    /// 授权码模式
    /// </summary>
    public class CodeInitConfig
    {

        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "用户获取API",new List<string>()
                {
                    IdentityModel.JwtClaimTypes.Role,"eMail"
                }),
                 new ApiResource("TestApi", "用户TestAPI",new List<string>()
                 {
                     IdentityModel.JwtClaimTypes.Role,"eMail"
                 })
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                     Username="cqq",
                     Password="123456",
                     SubjectId="0",
                     Claims = new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Cqq"),
                        new Claim("EMail","cqq123@qq.com")
                    }
                }
            };
        }



        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "QtechAuthDemo",//客户端唯一标识
                    ClientName="ApiClient for Code",
                    ClientSecrets = new [] { new Secret("cqq123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,//授权码
                    RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//可以多个
                    AllowedScopes = new [] { "UserApi","TestApi" },//允许访问的资源
                    AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
                }
            };
        }
    }
}

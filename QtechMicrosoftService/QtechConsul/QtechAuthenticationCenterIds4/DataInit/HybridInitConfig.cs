using IdentityServer4;
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
    /// 混合模式
    /// http://localhost:7200/connect/authorize?client_id=QtechAuthDemo&redirect_uri=http://localhost:5726/Ids4/IndexCodeToken&response_type=code%20token%20id_token&scope=UserApi%20openid%20CustomIdentityResource&response_model=fragment&nonce=12345
    /// </summary>
    public class HybridInitConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),//一堆默认属性
               new IdentityResource("CustomIdentityResource","This is Custom Model",
               new List<string>(){ "phonemodel","phoneprise", "eMail"})//自定义Id资源，植入claim

            };
        }


        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "用户获取API",new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" }),
                 new ApiResource("TestApi", "用户TestAPI",new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" })
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                     Username="qianqianchen",
                     Password="123456",
                     SubjectId="0",


                     Claims=new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Cqq"),
                        new Claim(ClaimTypes.Name,"apiUser"),
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
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowOfflineAccess = true,

                    ClientId = "QtechAuthDemo",//客户端惟一标识
                    ClientName="ApiClient for HyBrid",
                    ClientSecrets = new [] { new Secret("cqq123456".Sha256()) },
                    AccessTokenLifetime = 3600,//默认1小时
                    AllowedGrantTypes = GrantTypes.Hybrid,//混合模式
                    RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//回调网址

                    //允许访问的作用域
                    AllowedScopes = new [] {
                        "UserApi",
                        "TestApi",
                        IdentityServerConstants.StandardScopes.OpenId,//Ids4：获取Id_token，必需加入"openid"
                         IdentityServerConstants.StandardScopes.Profile,
                       "CustomIdentityResource"},//允许访问的资源
                    AllowAccessTokensViaBrowser = true//允许将token通过浏览器传递
                }
            };
        }
    }
}

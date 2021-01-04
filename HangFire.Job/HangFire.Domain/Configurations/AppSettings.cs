using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace HangFire.Domain.Configurations
{
    /// <summary>
    /// appsettings.json配置文件数据读取类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        /// <summary>
        /// 构造Appsetting
        /// </summary>
        static AppSettings()
        {
            //Loading appsetting.json file,Create IConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        /// <summary>
        /// System Version
        /// </summary>
        public static string Version => _config["Version"];

        /// <summary>
        /// Listen Port
        /// </summary>
        public static string ListenPort => _config["listenPort"];

        #region DBConnection
        /// <summary>
        /// Choose Hangfire Sql Connection
        /// </summary>
        public static string EnableDb => _config["ConnectionStrings:Enable"];

        /// <summary>
        /// Choose FaceImageApi Sql Connection
        /// </summary>
        public static string FaceImageEnableDb => _config["ConnectionStrings:FaceImageEnable"];

        /// <summary>
        /// ConnectionStrings Config
        /// </summary>
        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);

        #endregion

        #region AccessToken
        /// <summary>
        /// AccessToken Config
        /// </summary>
        public static class AccessToken
        {
            /// <summary>
            /// API_User
            /// </summary>
            public static string API_User => _config["AccessToken:API_User"];

            /// <summary>
            /// UserId
            /// </summary>
            public static int UserId =>Convert.ToInt32(_config["AccessToken:UserId"]);

            /// <summary>
            /// Client ID 
            /// </summary>
            public static string Client_ID => _config["AccessToken:Client_ID"];

            /// <summary>
            /// Client_Secret
            /// </summary>
            public static string Client_Secret => _config["AccessToken:Client_Secret"];

            /// <summary>
            /// Redirect Url
            /// </summary>
            public static string Redirect_Url => _config["AccessToken:Redirect_Url"];
        }
        #endregion

        #region RabbitMQ
        /// <summary>
        /// RabbitMQ
        /// </summary>
        public static class RabbitMQ
        {
            public static class Connections
            {
                public static class Default
                {
                    public static string Username => _config["RabbitMQ:Connections:Default:Username"];

                    public static string Password => _config["RabbitMQ:Connections:Default:Password"];

                    public static string HostName => _config["RabbitMQ:Connections:Default:HostName"];

                    public static int Port => Convert.ToInt32(_config["RabbitMQ:Connections:Default:Port"]);
                }
            }

            public static class EventBus
            {
                public static string ClientName => _config["RabbitMQ:EventBus:ClientName"];

                public static string ExchangeName => _config["RabbitMQ:EventBus:ExchangeName"];
            }
        }

        #endregion

        #region Cache Config
        /// <summary>
        /// Caching
        /// </summary>
        public static class Caching
        {
            public static bool IsOpen => Convert.ToBoolean(_config["Caching:IsOpen"]);

            public static string RedisConnectionString => _config["Caching:RedisConnectionString"];
        }
        #endregion

        #region JWT Config
        /// <summary>
        /// JWT
        /// </summary>
        public static class JWT
        {
            /// <summary>
            /// Domain
            /// </summary>
            public static string Domain => _config["JWT:Domain"];

            /// <summary>
            /// SecurityKey
            /// </summary>
            public static string SecurityKey => _config["JWT:SecurityKey"];

            /// <summary>
            /// Expires
            /// </summary>
            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);
        }
        #endregion

        #region Hangfire Config
        /// <summary>
        /// Hangfire
        /// </summary>
        public static class Hangfire
        {
            /// <summary>
            /// Login
            /// </summary>
            public static string Login => _config["Hangfire:Login"];

            /// <summary>
            /// Password
            /// </summary>
            public static string Password => _config["Hangfire:Password"];
        }
        #endregion

        #region Email Config
        /// <summary>
        /// Email
        /// </summary>
        public static class Email
        {
            /// <summary>
            /// Host
            /// </summary>
            public static string Host => _config["Email:Host"];

            /// <summary>
            /// Port
            /// </summary>
            public static int Port => Convert.ToInt32(_config["Email:Port"]);

            /// <summary>
            /// UseSsl
            /// </summary>
            public static bool UseSsl => Convert.ToBoolean(_config["Email:UseSsl"]);

            /// <summary>
            /// From
            /// </summary>
            public static class From
            {
                /// <summary>
                /// Username
                /// </summary>
                public static string Username => _config["Email:From:Username"];

                /// <summary>
                /// Password
                /// </summary>
                public static string Password => _config["Email:From:Password"];

                /// <summary>
                /// Name
                /// </summary>
                public static string Name => _config["Email:From:Name"];

                /// <summary>
                /// Address
                /// </summary>
                public static string Address => _config["Email:From:Address"];

            }

            /// <summary>
            /// To
            /// </summary>
            public static IDictionary<string, string> To
            {
                get
                {
                    var dic = new Dictionary<string, string>();

                    var emails = _config.GetSection("Email:To");
                    foreach (IConfigurationSection section in emails.GetChildren())
                    {
                        var name = section["Name"];
                        var address = section["Address"];

                        dic.Add(name, address);
                    }
                    return dic;
                }

            }
        }
        #endregion

        #region FaceImageApi Config
        /// <summary>
        /// FaceImageApi
        /// </summary>
        public static class FaceImageInterface
        {
            public static string TokenUrl => _config["FaceImageInterface:TokenUrl"];
            public static string CreateUserUrl => _config["FaceImageInterface:CreateUserUrl"];
            public static string DelLeaveEmpUrl => _config["FaceImageInterface:DelLeaveEmpUrl"];
            public static string GetSubjectIDUrl => _config["FaceImageInterface:GetSubjectIDUrl"];
            public static string UpdateEmpUrl => _config["FaceImageInterface:UpdateEmpUrl"];
            public static string LoginId => _config["FaceImageInterface:LoginId"];
            public static string LoginPsd => _config["FaceImageInterface:LoginPsd"];
        }

        #endregion
    }
}
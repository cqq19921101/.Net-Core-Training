using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace HangFire.Domain.Configuration
{
    /// <summary>
    /// appsetting.json配置文件
    /// </summary>
    public class Appsettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        /// <summary>
        /// 构造Appsetting
        /// </summary>
        static Appsettings()
        {
            //Loading appsetting.json file,Create IConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

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

        /// <summary>
        /// System Version
        /// </summary>
        public static string Version => _config["Version"];

        /// <summary>
        /// Listen Port
        /// </summary>
        public static string ListenPort => _config["listenPort"];

        #region Cache Config
        /// <summary>
        /// Cache
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
            public static string Domain => _config["JWT:Domain"];
            public static string SecurityKey => _config["JWT:SecurityKey"];
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

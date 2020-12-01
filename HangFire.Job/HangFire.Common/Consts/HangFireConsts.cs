using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Common.Consts
{
    /// <summary>
    /// 全局常量
    /// </summary>
    public class HangFireConsts
    {
        /// <summary>
        /// 数据库表前缀
        /// </summary>
        public const string DbTablePrefix = "hangfire_";

        /// <summary>
        /// 缓存过期时间策略
        /// </summary>
        public static class CacheStrategy
        {
            /// <summary>
            /// 一天过期24小时
            /// </summary>

            public const int ONE_DAY = 1440;

            /// <summary>
            /// 12小时过期
            /// </summary>

            public const int HALF_DAY = 720;

            /// <summary>
            /// 8小时过期
            /// </summary>

            public const int EIGHT_HOURS = 480;

            /// <summary>
            /// 5小时过期
            /// </summary>

            public const int FIVE_HOURS = 300;

            /// <summary>
            /// 3小时过期
            /// </summary>

            public const int THREE_HOURS = 180;

            /// <summary>
            /// 2小时过期
            /// </summary>

            public const int TWO_HOURS = 120;

            /// <summary>
            /// 1小时过期
            /// </summary>

            public const int ONE_HOURS = 60;

            /// <summary>
            /// 半小时过期
            /// </summary>

            public const int HALF_HOURS = 30;

            /// <summary>
            /// 5分钟过期
            /// </summary>
            public const int FIVE_MINUTES = 5;

            /// <summary>
            /// 1分钟过期
            /// </summary>
            public const int ONE_MINUTE = 1;

            /// <summary>
            /// 永不过期
            /// </summary>

            public const int NEVER = -1;
        }

        /// <summary>
        /// 缓存前缀
        /// </summary>
        public static class CachePrefix
        {
            public const string Authorize = "Authorize";

            public const string Job = "Job";

            public const string Job_Post = Job + ":Post";

            public const string Job_Category = Job + ":Category";

        }

        /// <summary>
        /// 响应文本
        /// </summary>
        public static class ResponseText
        {
            /// <summary>
            /// 新增成功
            /// </summary>
            public const string INSERT_SUCCESS = "新增成功";

            /// <summary>
            /// 更新成功
            /// </summary>
            public const string UPDATE_SUCCESS = "更新成功";

            /// <summary>
            /// 删除成功
            /// </summary>
            public const string DELETE_SUCCESS = "删除成功";

            /// <summary>
            /// 什么不存在
            /// </summary>
            public const string WHAT_NOT_EXIST = "{0}:{1} 不存在";

            /// <summary>
            /// 数据为空
            /// </summary>
            public const string DATA_IS_NONE = "数据为空";

            /// <summary>
            /// 密码错误
            /// </summary>
            public const string PASSWORD_WRONG = "密码错误";

            /// <summary>
            /// 参数错误
            /// </summary>
            public const string PARAMETER_ERROR = "参数错误";
        }
    }
}

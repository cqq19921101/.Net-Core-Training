using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace HangFire.Common.Helper
{
    public static class LoggerHelper
    {
        private static readonly ILoggerRepository Repository = LogManager.CreateRepository("NETCoreRepository");
        private static readonly ILog Log = LogManager.GetLogger(Repository.Name, "NETCorelog4net");

        static LoggerHelper()
        {
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void WriteToFile(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void WriteToFile(string message, Exception ex)
        {
            if (string.IsNullOrEmpty(message))
                message = ex.Message;

            Log.Error(message, ex);
        }

        /// <summary>
        /// 写本机Log
        /// </summary>
        /// <param name="errFlag"></param>
        /// <param name="msg"></param>
        public static void WriteErrorLog(string msg)
        {
            string errLogPath;
            msg = "[ERROR] " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ") + msg;
            errLogPath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrLog\\";
            string logFile = errLogPath + DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HH") + ".txt";


            if (!Directory.Exists(errLogPath))
            {
                Directory.CreateDirectory(errLogPath);
            }

            if (!File.Exists(logFile))
            {
                StreamWriter sw = File.CreateText(logFile);
                try
                {
                    sw.WriteLine(msg);
                }
                catch (Exception)
                {
                }
                finally
                {
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                //文件存在則複寫
                StreamWriter sw = File.AppendText(logFile);
                try
                {
                    sw.WriteLine(msg);
                }
                catch (Exception)
                {
                }
                finally
                {
                    sw.Flush();
                    sw.Close();
                }
            }
        }

    }
}
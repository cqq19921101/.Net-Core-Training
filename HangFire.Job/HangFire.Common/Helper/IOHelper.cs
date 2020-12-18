using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HangFire.Common.Helper
{
    /// <summary>
    /// IO帮助类
    /// </summary>
    public static class IOHelper
    {
        //是否已经加载了JPEG编码解码器
        private static bool _isloadjpegcodec = false;
        //当前系统安装的JPEG编码解码器
        private static ImageCodecInfo _jpegcodec = null;

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// 确定给定路径是否引用磁盘上的现有目录
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool DirectoryExists(string filename)
        {
            return System.IO.Directory.Exists(filename);
        }

        /// <summary>
        /// 在指定路径创建所有目录和子目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static void CreateDirectory(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Creates a new directory if it does not exists.
        /// </summary>
        /// <param name="directory">Directory to create</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 返回指定路径字符串的目录信息
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetDirectoryName(string filename)
        {
            return System.IO.Path.GetDirectoryName(filename);
        }

        /// <summary>
        /// 返回指定的路径字符串的扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetExtension(string filename)
        {
            return System.IO.Path.GetExtension(filename);
        }

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetFileName(string filename)
        {
            return System.IO.Path.GetFileName(filename);
        }

        /// <summary>
        /// 文件大小格式化
        /// </summary>
        /// <param name="fileSize">大小</param>
        /// <returns></returns>
        public static String FormatFileSize(long fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }

        #region 文件读取

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string GetStreamReader(string path, string encoding = "")
        {
            string str = string.Empty;
            StreamReader reader = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(encoding))
                {
                    reader = new StreamReader(path, System.Text.Encoding.GetEncoding(encoding));
                }
                else
                {
                    reader = new StreamReader(path, System.Text.Encoding.Default);
                }
                str = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return str;
        }

        #endregion

        #region  序列化

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="obj">序列对象</param>
        /// <param name="filePath">XML文件路径</param>
        /// <returns>是否成功</returns>
        public static bool SerializeToXml(object obj, string filePath)
        {
            bool result = false;

            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return result;

        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <param name="type">目标类型(Type类型)</param>
        /// <param name="filePath">XML文件路径</param>
        /// <returns>序列对象</returns>
        public static object DeserializeFromXML(Type type, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }



        /// <summary>
        /// 反序列化一个对象
        /// </summary>
        /// <param name="type">目标类型(Type类型)</param>
        /// <param name="s">字符串内容</param>
        /// <returns>序列化后的对象</returns>
        public static object DeserializeFromString(Type type, string s)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(s)))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion


        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}

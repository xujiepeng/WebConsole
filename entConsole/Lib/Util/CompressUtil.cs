using System;
using System.IO;
using System.IO.Compression;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-17
 * Content: 压缩工具类（基于System.IO.Compression.ZipFile）
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 压缩工具类（基于System.IO.Compression.ZipFile）
    /// </summary>
    public class CompressUtil
    {
        #region zip压缩文件
        /// <summary>
        /// zip压缩文件
        /// </summary>
        /// <param name="zipFileName">压缩包文件名(压缩包存在会先删除)</param>
        /// <param name="sourceFiles">源文件名</param>
        /// <returns>压缩包压缩是否成功</returns>
        public static bool ZipFiles(string zipFileName, params string[] sourceFiles)
        {
            bool result = true;

            if (string.IsNullOrEmpty(zipFileName) || sourceFiles == null)
            {
                return false;
            }

            if (sourceFiles.Length <= 0)
            {
                return false;
            }

            try
            {
                //压缩包存在，先删除
                if (File.Exists(zipFileName))
                {
                    File.Delete(zipFileName);
                }

                using (ZipArchive zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (string filename in sourceFiles)
                    {
                        zip.CreateEntryFromFile(filename, Path.GetFileName(filename), CompressionLevel.Optimal);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
        #endregion

        #region Zip压缩目录
        /// <summary>
        /// Zip压缩目录
        /// </summary>
        /// <param name="zipFileName">压缩包文件名</param>
        /// <param name="sourceDir">源目录名</param>
        /// <param name="includeBaseDir">压缩包中是否包含基目录</param>
        /// <returns>压缩包压缩是否成功</returns>
        public static bool ZipDir(string zipFileName, string sourceDir, bool isIncludeBaseDir = true)
        {
            bool result = true;

            if (string.IsNullOrEmpty(zipFileName) || !Directory.Exists(sourceDir))
            {
                return false;
            }

            try
            {
                //压缩包存在，先删除
                if (File.Exists(zipFileName))
                {
                    File.Delete(zipFileName);
                }

                //最佳压缩率，耗时可能会比较长
                ZipFile.CreateFromDirectory(sourceDir, zipFileName, CompressionLevel.Optimal, isIncludeBaseDir);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
        #endregion

        #region zip解压文件
        /// <summary>
        /// zip解压文件
        /// </summary>
        /// <param name="targetDir">解压目录(不存在自动创建)</param>
        /// <param name="zipFileName">压缩包文件名</param>
        /// <returns>解压结果是否成功</returns>
        public static bool UnZipFile(string targetDir, string zipFileName)
        {
            bool result = true;

            if (!File.Exists(zipFileName))
            {
                return false;
            }
                        
            try
            {
                //解压目录不存在，自动创建
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                //解压，同名文件覆盖
                ZipFile.ExtractToDirectory(zipFileName, targetDir, true);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
        #endregion
    }
}

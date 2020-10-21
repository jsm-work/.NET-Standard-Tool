using System;
using System.IO.Compression;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace Compress
{
    public class Zip
    {
        #region Compress
        #region System.IO.Compression
        /// <summary>
        /// 압축 - System.IO.Compression
        /// </summary>
        /// <param name="sourcePath">압축 대상 경로</param>
        /// <param name="zipPath">압축 파일 저장 경로</param>
        public static void Compression(string sourcePath, string zipPath)
        {
            string[] files = new System.IO.DirectoryInfo(sourcePath).GetFiles("*.*", System.IO.SearchOption.AllDirectories).Select(s => s.FullName).ToArray();

            using (System.IO.FileStream fileStream = new System.IO.FileStream(zipPath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
            {
                using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    foreach (string file in files)
                    {
                        string path = file.Substring(sourcePath.Length + 1);
                        zipArchive.CreateEntryFromFile(file, path);
                    }
                }
            }
        }

        /// <summary>
        /// 압축 해제 - System.IO.Compression
        /// </summary>
        /// <param name="zipPath">압축 파일 경로</param>
        /// <param name="destinationPath">압축 해제될 경로</param>
        public static void Decompression(string zipPath, string destinationPath, bool overwrite = false)
        { 
            if (!System.IO.Directory.Exists(destinationPath))
            {
                System.IO.Directory.CreateDirectory(destinationPath);
            }
            using (System.IO.Compression.ZipArchive zip = System.IO.Compression.ZipFile.OpenRead(zipPath))
            {
                foreach (System.IO.Compression.ZipArchiveEntry entry in zip.Entries)
                {
                    var filepath = System.IO.Path.Combine(destinationPath, entry.FullName);
                    var subDir = System.IO.Path.GetDirectoryName(filepath);
                    if (!System.IO.Directory.Exists(subDir))
                    {
                        System.IO.Directory.CreateDirectory(subDir);
                    }
                    entry.ExtractToFile(filepath, overwrite);
                }
            }
        }
        #endregion

        #region ICSharpCode.SharpZipLib.Zip
        /// <summary>
        /// 폴더 압축 - ICSharpCode.SharpZipLib.Zip
        /// </summary>
        /// <param name="sourceDirectoryPath">소스 디렉토리 경로</param>
        /// <param name="targetFilePath">타겟 ZIP 파일 경로</param>
        public static void CompressDirectory(string sourceDirectoryPath, string targetFilePath)
        {

            System.IO.DirectoryInfo sourceDirectoryInfo = new System.IO.DirectoryInfo(sourceDirectoryPath);
            sourceDirectoryInfo.Attributes = System.IO.FileAttributes.Archive;
            System.IO.FileStream targetFileStream = new System.IO.FileStream(targetFilePath, System.IO.FileMode.Create);
            ZipOutputStream zipOutputStream = new ZipOutputStream(targetFileStream);
            zipOutputStream.SetComment(sourceDirectoryPath);
            zipOutputStream.SetLevel(9);
            byte[] bufferByteArray = new byte[2048];
            foreach (System.IO.FileInfo sourceFileInfo in sourceDirectoryInfo.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                string sourceFileName = sourceFileInfo.FullName.Substring(sourceDirectoryInfo.FullName.Length + 1);
                zipOutputStream.PutNextEntry(new ZipEntry(sourceFileName));
                using (System.IO.FileStream sourceFileStream = sourceFileInfo.OpenRead())
                {
                    while (true)
                    {
                        int readCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                        if (readCount == 0)
                        {
                            break;
                        }
                        zipOutputStream.Write(bufferByteArray, 0, readCount);
                    }
                }
                zipOutputStream.CloseEntry();
            }
            zipOutputStream.Finish();
            zipOutputStream.Close();
        }

        /// <summary>
        /// 다중 파일 압축 - ICSharpCode.SharpZipLib.Zip
        /// </summary>
        /// <param name="sourceDirectoryPath">소스 디렉토리 경로</param>
        /// <param name="targetFilePath">타겟 ZIP 파일 경로</param>
        public static void CompressFilesToFile(string[] sourceFilePath, string targetFilePath)
        {
            System.IO.FileStream targetFileStream = new System.IO.FileStream(targetFilePath, System.IO.FileMode.Create);
            ZipOutputStream zipOutputStream = new ZipOutputStream(targetFileStream);
            zipOutputStream.SetLevel(9);
            byte[] bufferByteArray = new byte[2048];
            foreach (string filePath in sourceFilePath)
            {
                System.IO.FileInfo sourceFileInfo = new System.IO.FileInfo(filePath);
                string sourceFileName = sourceFileInfo.FullName.Substring(filePath.Length + 1);
                zipOutputStream.PutNextEntry(new ZipEntry(sourceFileName));
                using (System.IO.FileStream sourceFileStream = sourceFileInfo.OpenRead())
                {
                    while (true)
                    {
                        int readCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                        if (readCount == 0)
                        {
                            break;
                        }
                        zipOutputStream.Write(bufferByteArray, 0, readCount);
                    }
                }
                zipOutputStream.CloseEntry();
            }
            zipOutputStream.Finish();
            zipOutputStream.Close();
        }

        /// <summary>
        /// 파일 압축 해제 - ICSharpCode.SharpZipLib.Zip
        /// </summary>
        /// <param name="sourceFilePath">소스 파일 경로</param>
        /// <param name="targetDirectoryPath">타겟 디렉토리 경로</param>
        public static void DecompressFileToFile(string sourceFilePath, string targetDirectoryPath)
        {
            System.IO.DirectoryInfo targetDirectoryInfo = new System.IO.DirectoryInfo(targetDirectoryPath);

            if (!targetDirectoryInfo.Exists)
            {
                targetDirectoryInfo.Create();
            }

            System.IO.FileStream sourceFileStream = new System.IO.FileStream(sourceFilePath, System.IO.FileMode.Open);
            ZipInputStream zipInputStream = new ZipInputStream(sourceFileStream);

            byte[] bufferByteArray = new byte[2048];

            while (true)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(sourceFilePath);

                ZipEntry zipEntry = zipInputStream.GetNextEntry();

                if (zipEntry == null)
                {
                    break;
                }

                if (zipEntry.Name.LastIndexOf('\\') > 0)
                {
                    string subdirectory = fi.Name;

                    if (!System.IO.Directory.Exists(System.IO.Path.Combine(targetDirectoryInfo.FullName, subdirectory)))
                    {
                        targetDirectoryInfo.CreateSubdirectory(subdirectory);
                    }
                }

                System.IO.FileInfo targetFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(targetDirectoryInfo.FullName, fi.Name));

                using (System.IO.FileStream targetFileStream = targetFileInfo.Create())
                {
                    while (true)
                    {
                        int readCount = zipInputStream.Read(bufferByteArray, 0, bufferByteArray.Length);

                        if (readCount == 0)
                        {
                            break;
                        }

                        targetFileStream.Write(bufferByteArray, 0, readCount);
                    }
                }
            }
            zipInputStream.Close();
        }
        #endregion
        #endregion
    }
}

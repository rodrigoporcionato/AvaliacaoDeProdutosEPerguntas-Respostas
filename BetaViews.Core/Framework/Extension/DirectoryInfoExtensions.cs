using System.Collections.Generic;
using System.Linq;

namespace BetaViews.Core.Framework.Extension
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class DirectoryInfoExtensions
    {



        public static bool CheckFolderIsDenied(this string path)
        {
            try
            {
                var folderCheck = Directory.GetDirectories(path);

                return false;
            }
            catch (UnauthorizedAccessException)
            {
                // do something and return
                return true;
            }

            catch (Exception) {
                return true;
            
            }
        }


        /// <summary>
        /// deleta uma estrutura de pastas
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(this string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch
            {                
                //Directory.Delete(path, true);                
            }
            //catch (UnauthorizedAccessException)
            //{
            //    Directory.Delete(path, true);
            //}
        }

        public static bool CheckIfFileExists(this string filePathLocation) {

            if (File.Exists(filePathLocation))
            {
                return true;
            }
            return false;
        }


        public static bool CheckIfFolderExist(this string FolderPath) {

            if (Directory.Exists(FolderPath))
            {
                return true;
            }
            return false;
        
        }
        


        public static bool RemoveFile(this string filePathLocation) {

            if (File.Exists(filePathLocation))
            {
                try
                {
                    File.Delete(filePathLocation);
                    return true;
                }
                catch (System.IO.IOException e)
                {
                    string msg = !e.Message.IsEmptyOrWhiteSpace() ? e.Message : "";
                    LogFile.WriteLog("erro ao deletar o arquivo" + msg, "RemoveFile");
                    return false;
                }                
            }
            return false;        
        }

        public static bool HasFile(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }
       

        /// <summary>
        /// verifica se a imagem é jpg, gif, ou png
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CheckIfFileIsImageFormat(this HttpPostedFileBase file) {

            if (file.HasFile())
            {
                string fileExtension = file.FileName.Right(4);
                Regex reg = new Regex(@"\.(jpg|gif|png)$");
                if (reg.Match(fileExtension).Success) return true;
            }                                        
                return false;
        }


        /// <summary>
        /// Pega o arquivo atual e converte para um tipo GUID string.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string AddNewGuidNameToFile(this HttpPostedFileBase file) {

            if (file.HasFile())
            {
                string fileNewName = string.Format("{0}-{1}", file.FileName, Guid.NewGuid().ToString());
                string fileExtension = Path.GetExtension(file.FileName);
                fileNewName = string.Format("{0}{1}", fileNewName, fileExtension);

                return fileNewName;
            }

            return "";            
        
        }


        
        /// <summary>
        /// substitui o nome do arquivo por guid.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string AddNewGuidNameToFile2(this HttpPostedFileBase file)
        {

            if (file.HasFile())
            {
                string fileNewName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(file.FileName);
                fileNewName = string.Format("{0}{1}", fileNewName, fileExtension);

                return fileNewName;
            }

            return "";

        }



        /// <summary>
        /// Substitui o nome do arquivo atual adicionando um numero para o documento
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string AddNewNameForDocument(this HttpPostedFileBase file)
        {


            if (file.HasFile())
            {
                Random rnd = new Random();
                string fileNewName = string.Format("{0}_{1}", file.FileName.Left(file.FileName.Length - 4), rnd.Next(100, 1000).ToString());
                string fileExtension = Path.GetExtension(file.FileName);
                fileNewName = string.Format("{0}{1}", fileNewName, fileExtension);

                return fileNewName;
            }

            return "";

        }


        public static string AddNewGuidNameToFileToPDF(this HttpPostedFileBase file)
        {

            if (file.HasFile())
            {

                string fileNewName = Guid.NewGuid().ToString();
                //string fileExtension = Path.GetExtension(file.FileName);
                fileNewName = string.Format("{0}{1}", fileNewName, ".pdf");

                return fileNewName;
            }

            return "";

        }


        /// <summary>
        /// Copies all files from one directory to another.
        /// <remarks>
        /// Contributed by Christian Liensberger, 
        /// http://www.liensberger.it/
        /// </remarks>
        /// </summary>
        public static void CopyTo(this DirectoryInfo source, string destination, bool recursive)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            DirectoryInfo target = new DirectoryInfo(destination);
            if (!source.Exists)
            {
                throw new DirectoryNotFoundException("Source directory not found: " + source.FullName);
            }
            if (!target.Exists)
            {
                target.Create();
            }

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            if (!recursive)
            {
                return;
            }

            foreach (var directory in source.GetDirectories())
            {
                CopyTo(directory, Path.Combine(target.FullName, directory.Name), recursive);
            }
        }



        /// <summary>
        /// Move os arquivos de um diretorio para outro.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="recursive"></param>
        public static void MoveFileTo(this DirectoryInfo source, string destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            DirectoryInfo target = new DirectoryInfo(destination);
            if (!source.Exists)
            {
                throw new DirectoryNotFoundException("Source directory not found: " + source.FullName);
            }
            if (!target.Exists)
            {
                target.Create();
            }

            foreach (var file in source.GetFiles())
            {
                //file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                if (File.Exists(Path.Combine(target.FullName, file.Name)))
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    file.Delete();
                }
                else
                {
                    file.MoveTo(Path.Combine(target.FullName, file.Name));
                }

            }           
        }




        /// <summary>
        /// 	Gets all files in the directory matching one of the several (!) supplied patterns (instead of just one in the regular implementation).
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "patterns">The patterns.</param>
        /// <returns>The matching files</returns>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 	</code>
        /// </example>
        public static FileInfo[] GetFiles(this DirectoryInfo directory, params string[] patterns)
        {
            var files = new List<FileInfo>();
            foreach (var pattern in patterns)
                files.AddRange(directory.GetFiles(pattern));
            return files.ToArray();
        }

        /// <summary>
        /// 	Searches the provided directory recursively and returns the first file matching the provided pattern.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "pattern">The pattern.</param>
        /// <returns>The found file</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive("win.ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, string pattern)
        {
            var files = directory.GetFiles(pattern);
            if (files.Length > 0)
                return files[0];

            foreach (var subDirectory in directory.GetDirectories())
            {
                var foundFile = subDirectory.FindFileRecursive(pattern);
                if (foundFile != null)
                    return foundFile;
            }
            return null;
        }

        /// <summary>
        /// 	Searches the provided directory recursively and returns the first file matching to the provided predicate.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "predicate">The predicate.</param>
        /// <returns>The found file</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive(f => f.Extension == ".ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
        {
            foreach (var file in directory.GetFiles())
            {
                if (predicate(file))
                    return file;
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                var foundFile = subDirectory.FindFileRecursive(predicate);
                if (foundFile != null)
                    return foundFile;
            }
            return null;
        }

        /// <summary>
        /// 	Searches the provided directory recursively and returns the all files matching the provided pattern.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "pattern">The pattern.</param>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <returns>The found files</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var files = directory.FindFilesRecursive("*.ini");
        /// 	</code>
        /// </example>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, string pattern)
        {
            var foundFiles = new List<FileInfo>();
            FindFilesRecursive(directory, pattern, foundFiles);
            return foundFiles.ToArray();
        }

        static void FindFilesRecursive(DirectoryInfo directory, string pattern, List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles(pattern));
            directory.GetDirectories().ToList().ForEach(d => FindFilesRecursive(d, pattern, foundFiles));
        }

        /// <summary>
        /// 	Searches the provided directory recursively and returns the all files matching to the provided predicate.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "predicate">The predicate.</param>
        /// <returns>The found files</returns>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var files = directory.FindFilesRecursive(f => f.Extension == ".ini");
        /// 	</code>
        /// </example>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
        {
            var foundFiles = new List<FileInfo>();
            FindFilesRecursive(directory, predicate, foundFiles);
            return foundFiles.ToArray();
        }

        static void FindFilesRecursive(DirectoryInfo directory, Func<FileInfo, bool> predicate, List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles().Where(predicate));
            directory.GetDirectories().ToList().ForEach(d => FindFilesRecursive(d, predicate, foundFiles));
        }
    
    }
}

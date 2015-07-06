using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessAccessLayer;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RandomFiles
{
    public class Repository
    {
        public Repository()
        {
        }

        public void UploadFiles(Folder from, Folder to)
        {
        }

        public void GetFiles(Folder f)
        {
        }

        #region Properties

        private static string _RegexExtensions;
        public static string RegexExtensions
        {
            get
            {
                return @"\.(" + string.Join("|", Extensions) + ")$";
            }
            set
            {
                _RegexExtensions = value;
            }
        }

        private static string[] _Extensions = new string[] { "mp3", "flac"/*, "mp2" */};
        public static string[] Extensions
        {
            get
            {
                return _Extensions;
            }
        }

        #endregion

        public static string GetSize(double size)
        {
            int i;

            for (i = 0; size > 1000; i++)
            {
                size /= 1024;
            }

            var prefixes = new[] { "bytes", "KB", "MB", "GB", "TB", "PB", "EB" };

            return string.Format("{0:##0.##} {1}", size, prefixes[i]);
        }

        public static bool HasFolders(string path)
        {
            if (IsFolderAccessible(path))
            {
                return Directory.GetDirectories(path).Any();
            }
            else
            {
                return false;
            }
        }

        public static bool IsDriveRemovable(string path)
        {
            return new DriveInfo(Path.GetPathRoot(path)).DriveType == DriveType.Removable;
        }

        public static IEnumerable<FileInfo> GetFiles(string path)
        {
            return Directory.GetFiles(path, "*.*").Where(file => Regex.Match(file, RegexExtensions, RegexOptions.IgnoreCase).Success).Select(file => new FileInfo(file));
        }

        public static IEnumerable<FileInFolder> GetFilesInFolder(string path)
        {
            return Directory.GetFiles(path, "*.*").Where(file => Regex.Match(file, RegexExtensions, RegexOptions.IgnoreCase).Success).Select(file => FileInFolder.Create(new FileInfo(file)));
        }

        public static IEnumerable<Folder> GetFolders(string rootPath)
        {
            if (Directory.Exists(rootPath))
            {
                List<string> dirs = Directory.GetDirectories(rootPath).ToList<string>();

                foreach (string dir in dirs)
                {
                    if (IsFolderAccessible(dir))
                    {
                        var subFolders = HasFolders(dir);
                        yield return Folder.Create(dir, subFolders);
                    }
                }
            }
        }

        public static IEnumerable<FileInFolder> GetFiles(string rootPath, string[] extensions)
        {
            if (Directory.Exists(rootPath))
            {
                List<string> dirs = Directory.GetDirectories(rootPath).ToList<string>();

                foreach (string dir in dirs)
                {
                    if (IsFolderAccessible(dir))
                    {
                        if (GetFiles(dir).Any())
                        {
                            foreach (FileInfo file in GetFiles(dir))
                                yield return FileInFolder.Create(file);
                        }
                    }
                }
            }
        }

        private IEnumerable<Folder> RecursePath(string rootPath)
        {
            List<string> dirs = Directory.GetDirectories(rootPath).ToList<string>();

            foreach (string dir in dirs)
            {
                var subFolders = RecursePath(dir);

                yield return Folder.Create(dir, subFolders.Any());
            }
        }

        List<FileInFolder> list = new List<FileInFolder>();

        public IEnumerable<FileInFolder> RecurseFiles(string Path, bool GetFilesByPath)
        {
            List<string> dirs = Directory.GetDirectories(Path).ToList<string>();

            if (GetFiles(Path).Any() && GetFilesByPath)
            {
                foreach (FileInfo file in GetFiles(Path))
                    list.Add(FileInFolder.Create(file));
            }

            foreach (string dir in dirs)
            {
                if (IsFolderAccessible(dir))
                {
                    if (GetFiles(dir).Any())
                    {
                        foreach (FileInfo file in GetFiles(dir))
                            list.Add(FileInFolder.Create(file));
                    }
                }

                if (HasFolders(dir))
                    RecurseFiles(dir, false);
            }

            return list.AsEnumerable();
        }

        private static bool IsFolderAccessible(string dir)
        {
            DirectoryInfo currentDir = new DirectoryInfo(dir);

            bool accessible = true;
            try
            {
                currentDir.GetDirectories().Any();
            }
            catch (Exception ex)
            {
                accessible = false;
            }

            return accessible;
        }
    }
}

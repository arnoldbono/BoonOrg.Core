// (c) 2017 Roland Boon

using System;
using System.IO;
using System.Collections.Generic;

namespace BoonOrg.Storage.Domain
{
    internal sealed class FilePathCleanup : IFilePathCleanup
    {
        public string Cleanup(IDocument document, string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                var info = document.DocumentInfo;
                if (info != null)
                {
                    int index = info.FilePath.LastIndexOf(Path.DirectorySeparatorChar);
                    if (index > 0)
                    {
                        string folder = info.FilePath.Substring(0, index);
                        if (filePath.StartsWith(@$".{Path.DirectorySeparatorChar}"))
                        {
                            filePath = Path.Combine(folder, filePath.Substring(2));
                        }
                        else if (!filePath.Contains(Path.DirectorySeparatorChar))
                        {
                            filePath = Path.Combine(folder, filePath.Substring(1));
                        }
                    }
                }
            }

            return filePath;
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}

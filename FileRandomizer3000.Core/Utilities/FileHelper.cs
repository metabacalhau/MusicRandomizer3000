using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Utilities;
using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileRandomizer3000.Core.Utilities
{
    public class FileHelper : IFileHelper
    {
        public string FileSizeFormatter(double size)
        {
            string formattedSize = "";

            if (size >= 0)
            {
                int i;

                for (i = 0; size > 1000; i++)
                {
                    size /= 1024;
                }

                string[] prefixes = new[] { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

                formattedSize = string.Format(CultureInfo.CurrentCulture, "{0:##0.##} {1}", size, prefixes[i]);
            }

            return formattedSize;
        }

        public double FileSizeConverter(double size, FileSizeUnit unit)
        {
            double result = 0;

            if (size > 0)
            {
                if (unit == FileSizeUnit.Megabyte)
                {
                    result = size * 1024 * 1024;
                }
                else if (unit == FileSizeUnit.Gigabyte)
                {
                    result = size * 1024 * 1024 * 1024;
                }
            }

            return result;
        }

        public string GenerateUniqueFileName(AppFile file, IUniqueCharsGenerator generator, UniqueCharsPosition position)
        {
            if (file == null) { throw new ArgumentNullException("file"); }
            if (generator == null) { throw new ArgumentNullException("generator"); }

            string uniqueID = generator.Generate();

            StringBuilder sb = new StringBuilder();

            if (position == UniqueCharsPosition.Prefix)
            {
                sb.AppendFormat("{0}_", uniqueID);
            }

            sb.AppendFormat("{0}", file.FileNameWithoutExtension);

            if (position == UniqueCharsPosition.Suffix)
            {
                sb.AppendFormat("_{0}", uniqueID);
            }

            sb.AppendFormat("{0}", file.FileExtension);

            return sb.ToString();
        }

        public AppFile GetRandomFile(IEnumerable<AppFile> files)
        {
            return files.PickRandom();
        }
    }
}
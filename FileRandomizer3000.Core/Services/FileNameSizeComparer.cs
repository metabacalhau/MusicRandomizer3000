using FileRandomizer3000.Core.Models;
using System;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services
{
    public class FileNameSizeComparer : IEqualityComparer<AppFile>
    {
        public bool Equals(AppFile x, AppFile y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.FileNameFull == y.FileNameFull && x.FileSize == y.FileSize;
        }

        public int GetHashCode(AppFile obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            string fileNameFull = obj.FileNameFull == null ? "" : obj.FileNameFull;

            return fileNameFull.GetHashCode() ^ obj.FileSize.GetHashCode();
        }
    }
}

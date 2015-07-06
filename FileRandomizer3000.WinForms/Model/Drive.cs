using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccessLayer
{
    class Drive
    {
        public Drive()
        {

        }

        public string DriveLabel { get; set; }
        public long DriveSize { get; set; }
        public bool HasFolders { get; set; }
    }
}

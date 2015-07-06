using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Diagnostics;

namespace FileRandomizer3000.Core.Services
{
    public class ProcessWrapper : IProcessWrapper
    {
        public void Start(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            Process.Start(fileName);
        }
    }
}
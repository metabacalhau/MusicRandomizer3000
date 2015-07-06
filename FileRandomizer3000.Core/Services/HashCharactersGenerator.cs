using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Globalization;

namespace FileRandomizer3000.Core.Services
{
    public class HashCharactersGenerator : IUniqueCharsGenerator
    {
        public string Generate()
        {
            return DateTime.Now.GetHashCode().ToString(CultureInfo.CurrentCulture);
        }
    }
}
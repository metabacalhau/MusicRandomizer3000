using System;

namespace FileRandomizer3000.Core.Services
{
    public class GuidCharactersGenerator : BaseUniqueCharsGenerator
    {
        public GuidCharactersGenerator(int maxLength)
            : base(maxLength)
        {
            if (maxLength < 5) throw new ArgumentOutOfRangeException("maxLength");
            if (maxLength > 32) throw new ArgumentOutOfRangeException("maxLength");
        }

        public override string Generate()
        {
            UniqueString = Guid.NewGuid().ToString("N");

            return base.Generate();
        }
    }
}
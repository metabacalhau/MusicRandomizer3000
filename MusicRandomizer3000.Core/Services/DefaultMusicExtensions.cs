using MusicRandomizer3000.Core.Services.Interfaces;

namespace MusicRandomizer3000.Core.Services
{
    public class DefaultMusicExtensions : FileExtensionDecorator
    {
        public DefaultMusicExtensions() : this(null) { }
        public DefaultMusicExtensions(IFileExtension additionalExtensions) : base(additionalExtensions)
        {
            Allowed = new string[] { "*.mp3", "*.flac" };
        }
    }
}
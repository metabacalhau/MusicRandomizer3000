using MusicRandomizer3000.Core.Models;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface ISettingsService
    {
        T LoadSettings<T>(string parameterName) where T : IStepSetting;

        void SaveSettings<T>(string parameterName, T parameterValue) where T : IStepSetting;

        void RemoveSettings(string parameterName);
    }
}
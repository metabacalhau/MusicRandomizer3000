using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace FileRandomizer3000.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly SettingsBase _settings;

        public SettingsService(SettingsBase settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            _settings = settings;
        }

        public T LoadSettings<T>(string parameterName) where T : IStepSetting
        {
            T result = default(T);

            if (HasSettings(parameterName))
            {
                if (_settings[parameterName] != null)
                {
                    string parameterValue = (string)_settings[parameterName];

                    try
                    {
                        result = JsonConvert.DeserializeObject<T>(parameterValue);
                    }
                    catch (JsonSerializationException)
                    {
                        result = default(T);
                    }
                    catch (JsonReaderException)
                    {
                        result = default(T);
                    }
                }
            }

            return result;
        }

        public void SaveSettings<T>(string parameterName, T parameterValue) where T : IStepSetting
        {
            string serializedValue = JsonConvert.SerializeObject(parameterValue);

            _settings[parameterName] = serializedValue;

            _settings.Save();
        }

        public void RemoveSettings(string parameterName)
        {
            if (HasSettings(parameterName))
            {
                _settings[parameterName] = null;

                _settings.Save();
            }
        }

        private bool HasSettings(string parameterName)
        {
            try
            {
                return _settings[parameterName] != null;
            }
            catch (SettingsPropertyNotFoundException)
            {
                return false;
            }
        }
    }
}
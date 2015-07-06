using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class SettingsServiceTests
    {
        private class TestSettings : IStepSetting
        {
            public int TestValue { get; set; }
        }

        private class SettingsStub : SettingsBase
        {
            private readonly Dictionary<string, object> _propertyValues;

            public SettingsStub()
            {
                _propertyValues = new Dictionary<string, object>();
            }

            public override object this[string propertyName]
            {
                get
                {
                    if (_propertyValues.ContainsKey(propertyName))
                    {
                        return _propertyValues[propertyName];
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    if (_propertyValues.ContainsKey(propertyName))
                    {
                        _propertyValues[propertyName] = value;
                    }
                    else
                    {
                        _propertyValues.Add(propertyName, value);
                    }
                }
            }

            public override void Save()
            {
            }
        }

        [Test]
        public void SettingsService_SupplyNullSettingsArgument_ThrowsSettingsArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new SettingsService(null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settings"));
        }

        [Test]
        public void LoadSettings_SupplyInvalidSerializedSettingString_ReturnsDefaultValue()
        {
            LoadSettings_ValidatesIfIsExpectedValue<TestSettings>("", default(TestSettings));
            LoadSettings_ValidatesIfIsExpectedValue<TestSettings>("xcvdrdg4654654", default(TestSettings));
            LoadSettings_ValidatesIfIsExpectedValue<TestSettings>("1111111111", default(TestSettings));
        }

        [Test]
        public void LoadSettings_SupplyValidSerializedSettingsString_ReturnsValidObject()
        {
            // arrange
            TestSettings expectedResult = new TestSettings { TestValue = 44 };

            SettingsStub settingsStub = new SettingsStub();

            settingsStub["Test"] = JsonConvert.SerializeObject(expectedResult);

            SettingsService service = new SettingsService(settingsStub);

            // act
            TestSettings actualResult = service.LoadSettings<TestSettings>("Test");

            // assert
            AreEqualByJson(expectedResult, actualResult);
        }

        [Test]
        public void LoadSettings_ThrowsSettingsPropertyNotFoundException_ReturnsDefaultValue()
        {
            // arrange
            Mock<SettingsBase> settingsBaseMock = new Mock<SettingsBase>();

            settingsBaseMock.Setup(x => x["Test"]).Throws(new SettingsPropertyNotFoundException());

            SettingsService service = new SettingsService(settingsBaseMock.Object);

            // act
            TestSettings actualResult = service.LoadSettings<TestSettings>("Test");

            // assert
            Assert.AreEqual(default(TestSettings), actualResult);
        }

        [Test]
        public void SaveSettings_SupplyValueForNonExistingParameter_SuccessfullyAddsValue()
        {
            // arrange
            TestSettings actualValue = new TestSettings { TestValue = 44 };

            SettingsStub settingsStub = new SettingsStub();

            SettingsService service = new SettingsService(settingsStub);

            // act
            service.SaveSettings<TestSettings>("Test", actualValue);

            // assert
            Assert.AreEqual(JsonConvert.SerializeObject(actualValue), settingsStub["Test"]);
        }

        [Test]
        public void SaveSettings_SupplyValueForNonExistingParameter_SuccessfullyUpdatesValue()
        {
            // arrange
            TestSettings existingValue = new TestSettings { TestValue = 44 };
            string existingJsonValue = JsonConvert.SerializeObject(existingValue);

            SettingsStub settingsStub = new SettingsStub();
            settingsStub["Test"] = existingJsonValue;

            SettingsService service = new SettingsService(settingsStub);

            existingValue.TestValue = 55;
            existingJsonValue = JsonConvert.SerializeObject(existingValue);

            // act
            service.SaveSettings<TestSettings>("Test", existingValue);

            // assert
            Assert.AreEqual(existingJsonValue, settingsStub["Test"]);
        }

        [Test]
        public void RemoveSettings_SupplyExistingParameterName_DeletesSettings()
        {
            // arrange
            TestSettings existingValue = new TestSettings { TestValue = 44 };

            SettingsStub settingsStub = new SettingsStub();
            settingsStub["Test"] = JsonConvert.SerializeObject(existingValue);

            SettingsService service = new SettingsService(settingsStub);

            // act
            service.RemoveSettings("Test");

            // assert
            Assert.IsNull(settingsStub["Test"]);
        }

        [Test]
        public void RemoveSettings_ThrowsSettingsPropertyNotFoundException_DoesNotDeletesSettings()
        {
            // arrange
            Mock<SettingsBase> settingsBaseMock = new Mock<SettingsBase>();
            settingsBaseMock.Setup(x => x["Test"]).Throws(new SettingsPropertyNotFoundException());

            SettingsService service = new SettingsService(settingsBaseMock.Object);

            // act
            service.RemoveSettings("Test");

            // assert
            settingsBaseMock.VerifySet(x => x["Test"] = null, Times.Never);
        }

        private void LoadSettings_ValidatesIfIsExpectedValue<T>(string serializedSettingsValue, object expectedResult) where T : IStepSetting
        {
            SettingsStub settingsStub = new SettingsStub();

            settingsStub["Test"] = serializedSettingsValue;

            SettingsService service = new SettingsService(settingsStub);

            T actualResult = service.LoadSettings<T>("Test");

            Assert.AreEqual(expectedResult, actualResult);
        }

        private void AreEqualByJson(object expected, object actual)
        {
            string expectedJson = JsonConvert.SerializeObject(expected);

            string actualJson = JsonConvert.SerializeObject(actual);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
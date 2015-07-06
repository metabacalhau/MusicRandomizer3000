using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace FileRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class GlobalWizardViewModelTests
    {
        [Test]
        public void Constructor_CreatesRandomizerSettingsModel()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel(It.IsAny<string>());

            Assert.IsNotNull(model.RandomizerWorkerSettings);
        }

        [Test]
        public void Constructor_CreatesCopierSettingsModel()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel(It.IsAny<string>());

            Assert.IsNotNull(model.CopyWorkerSettings);
        }

        [Test]
        public void FormTitle_DoNotSetValue_ReturnsExpectedSuffix()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel("Suffix");

            Assert.AreEqual("Suffix", model.FormTitle);
        }

        [Test]
        public void FormTitle_SetValue_ReturnsSetValueWithSuffix()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel("Suffix");

            model.FormTitle = "Value";

            Assert.AreEqual("ValueSuffix", model.FormTitle);
        }

        [Test]
        public void FormTitle_SetValue_FiresPropertyChangedValue()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel(It.IsAny<string>());

            model.ShouldNotifyOn(s => s.FormTitle).When(s => s.FormTitle = "Some new value");
        }

        [Test]
        public void FormTitle_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            GlobalWizardViewModel model = new GlobalWizardViewModel("Suffix");
            model.FormTitle = "Suffix";

            model.ShouldNotNotifyOn(s => s.FormTitle).When(s => s.FormTitle = "Suffix");
        }
    }
}

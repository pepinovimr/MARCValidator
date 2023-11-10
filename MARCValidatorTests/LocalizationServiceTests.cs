using SharedLayer;
using System.Globalization;
using System.Resources;

namespace MARCValidatorTests
{
    /// <summary>
    /// Example unit test class for localization service
    /// </summary>
    [TestClass]
    public class LocalizationServiceTests
    {
        private Mock<ResourceManager> _resourceManagerMock;

        [TestInitialize]
        public void Setup()
        {
            _resourceManagerMock = new Mock<ResourceManager>();
            CultureInfo.CurrentCulture = new CultureInfo("cs");
        }
        
        [TestMethod]
        public void GetLocalizedValue_UsingProperKey_ReturnsItsValue()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string key = "ApplicationName";
            string expectedValue = "Ov��ova� MARC z�znam�";

            _resourceManagerMock.Setup(x => x.GetString(key)).Returns(expectedValue);

            string result = localizationService[key];

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void GetLocalizedValue_UsingWrongKey_ReturnsKey()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string key = "KeyThatDoesNotExist";

            string result = localizationService[key];

            Assert.AreEqual(key, result);
        }

        [TestMethod]
        public void SetCultureInfo_WithProperValue_ChangesCultureSuccessfully()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string newCulture = "en";

            localizationService.SetCultureInfo(newCulture);

            Assert.AreEqual(newCulture, CultureInfo.CurrentCulture.Name);
            Assert.AreEqual(newCulture, CultureInfo.CurrentUICulture.Name);
        }

        [TestMethod]
        public void SetCultureInfo_WithWrongValue_ThrowsException()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string newCulture = "0";    //Empty string is acceptable => 0

            Action setCulture = () => localizationService.SetCultureInfo(newCulture);

            Assert.ThrowsException<CultureNotFoundException>(setCulture);
        }

    }
}
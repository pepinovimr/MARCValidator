using SharedLayer;
using System.Globalization;
using System.Resources;

namespace MARCValidatorTests
{
    /// <summary>
    /// Example unit test class for localization service
    /// 
    /// Disclaimer: These tests are largly 
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
        public void Indexer_GetLocalizedValue_Success()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string key = "ApplicationName";
            string expectedValue = "Ovìøovaè MARC záznamù";

            _resourceManagerMock.Setup(x => x.GetString(key)).Returns(expectedValue);

            string result = localizationService[key];

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void Indexer_KeyNotFound_ReturnsKey()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string key = "KeyThatDoesNotExist";

            string result = localizationService[key];

            Assert.AreEqual(key, result);
        }

        [TestMethod]
        public void SetCultureInfo_ChangesCultureSuccessfully()
        {
            var localizationService = new LocalizationService(_resourceManagerMock.Object);
            string newCulture = "en";

            localizationService.SetCultureInfo(newCulture);

            Assert.AreEqual(newCulture, CultureInfo.CurrentCulture.Name);
            Assert.AreEqual(newCulture, CultureInfo.CurrentUICulture.Name);
        }
    }
}
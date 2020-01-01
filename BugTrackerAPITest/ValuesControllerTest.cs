using BugTrackerAPI.Controllers;
using BugTrackerAPI.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BugTrackerAPITest
{
    [TestClass]
    public class ValuesControllerTest
    {
        private Mock<IUtility> mock;
        private Mock<IBugRepository> mock1;
        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IUtility>();
            mock1 = new Mock<IBugRepository>();
        }
        [TestMethod]
        public void Test_GenerateToken_Method_Returns_Token()
        {
            ValuesController valuesController = new ValuesController(mock.Object,mock1.Object);
            mock.Setup(x => x.checkCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.GenerateJSONWebToken(It.IsAny<string>())).Returns("token");

            string Actual_result=valuesController.Token("RaviPagidoju", "");
            string Expected_result = "token";

            Assert.AreEqual(Expected_result, Actual_result);
        }
    }
}

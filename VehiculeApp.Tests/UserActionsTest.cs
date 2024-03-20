using Actions = MyLibrary.Utils.UserActions;
using NUnit.Framework;

namespace VehiculeApp.Tests
{
    public class Tests
    {
        [Test]
        public void GetVehiculeModelTest()
        {
            string userInput = "2024";

            string result = Actions.GetVehiculeModel();

            Assert.That(result, Is.EqualTo("2024"));
        }

        [Test]
        public void GetVehiculeWeightTest()
        {
            double userInput = 3094.34;

            double result = Actions.GetVehiculeWeight();

            Assert.That(result, Is.EqualTo(3094.34));
        }
    }
}
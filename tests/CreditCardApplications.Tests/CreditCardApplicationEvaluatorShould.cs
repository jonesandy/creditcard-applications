using Xunit;
using Moq;
using CreditCardApplications.Interfaces;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        Mock<IFrequentFlyerNumberValidator> _mockValidator;

        public CreditCardApplicationEvaluatorShould()
        {
            _mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        }

        [Trait("Decision", "AutoAccepted")]
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            // Examples of argument matching
            //_mockValidator.Setup(x => x.IsValid(It.Is<string>(number => number.StartsWith("y"))))
            //    .Returns(true);
            //_mockValidator.Setup(x => x.IsValid(It.IsInRange("a", "z", Range.Inclusive)))
            //    .Returns(true);
            //_mockValidator.Setup(x => x.IsValid(It.IsIn("x", "y", "z")))
            //    .Returns(true);

            var sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
            var application = new CreditCardApplication() 
                { 
                    GrossAnnualIncome = 100_000,
                    FrequentFlyerNumber = "y"
                };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Trait("Decision", "ReferredToAgent")]
        [Fact]
        public void ReferYoundApplicants()
        {
            var sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
            var application = new CreditCardApplication() { Age = 19, GrossAnnualIncome = 20_000 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToAgent, decision);
        }

        [Trait("Decision", "ReferredToAgent")]
        [Fact]
        public void ReferInvalidFrequentFlyerApplication()
        {
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            var sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
            var application = new CreditCardApplication();

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToAgent, decision);
        }

        [Trait("Decision", "AutoDeclined")]
        [Fact]
        public void DeclineLowIncomeApplications()
        {
            var sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
            var application = new CreditCardApplication() { GrossAnnualIncome = 19_999, Age = 42 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }
    }
}

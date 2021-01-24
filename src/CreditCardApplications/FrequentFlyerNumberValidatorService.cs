using CreditCardApplications.Interfaces;

namespace CreditCardApplications
{
    /// <summary>
    /// Faking a hard to use real service for purposes of course
    /// </summary>
    public class FrequentFlyerNumberValidatorService : IFrequentFlyerNumberValidator
    {
        public bool IsValid(string frequentFlyerNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new System.NotImplementedException();
        }
    }
}

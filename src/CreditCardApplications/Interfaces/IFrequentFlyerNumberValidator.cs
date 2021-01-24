namespace CreditCardApplications.Interfaces
{
    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);
        bool IsValid(string frequentFlyerNumber, out bool isValid);
    }
}

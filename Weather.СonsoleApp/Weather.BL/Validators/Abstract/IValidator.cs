namespace Weather.BL.Validators.Abstract
{
    public interface IValidator
    {
        void ValidateCityByName(string value);
        void ValidateForecast(string name, int days);
    }
}

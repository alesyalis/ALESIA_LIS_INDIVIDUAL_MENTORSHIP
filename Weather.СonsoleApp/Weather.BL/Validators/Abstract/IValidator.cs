namespace Weather.BL.Validators.Abstract
{
    public interface IValidator<T> where T : class
    {
        void ValidateCityName(T value);
    }
}

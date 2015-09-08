namespace Domain.BusinessRules.Interfaces
{
    public interface IBusinessRule<T> where T : class
    {
        bool IsSatisfied(T obj);
    }
}
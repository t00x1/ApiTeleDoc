namespace Domain.Interfaces.Common
{
    public interface IAutoMapper
    {
        public void CopyPropertiesTo<TSource, TDest>(TSource source, TDest dest);
    }
}

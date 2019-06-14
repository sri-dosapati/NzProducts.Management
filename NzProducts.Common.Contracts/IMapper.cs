
namespace NzProducts.Common.Contracts
{
    public interface IMapper<in TSource, out TDestination>
        where TSource : class
    {
        TDestination Map(TSource source);
    }
}

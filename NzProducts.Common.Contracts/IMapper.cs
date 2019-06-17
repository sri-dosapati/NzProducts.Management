
namespace NzProducts.Common.Contracts
{
    public interface IMapper<in TSource, out TDestination>
    {
        TDestination Map(TSource source);
    }
}

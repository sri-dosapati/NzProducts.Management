namespace NzProducts.Common.Contracts
{
    public interface IMappingTargetProvider<T>
    {
        T Create();
    }
}

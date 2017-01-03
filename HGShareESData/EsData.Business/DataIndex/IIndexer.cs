namespace EsData.Business.DataIndex
{
    public interface IIndexer
    {
        bool IsPaping { get; }
        void HandleData();
    }
}

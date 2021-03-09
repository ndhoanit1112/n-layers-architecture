using NC.Common;

namespace NC.BusinessModel.Shared
{
    public class PagedResult<TEntity> where TEntity : class
    {
        public TEntity[] Items { get; set; }

        public bool HasMoreRecords { get; set; }

        public int Totals { get; set; }

        public object ExtraData { get; set; }

        public static PagedResult<TEntity> Empty => new PagedResult<TEntity>
        {
            Items = EmptyArray<TEntity>.Instance,
        };
    }
}

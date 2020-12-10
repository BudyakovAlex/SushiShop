using System;

namespace SushiShop.Core.Data.Models.Pagination
{
    public class PaginationContainer<TBusinessObject>
    {
        public PaginationContainer(
            int currentLimit,
            int currentOffset,
            int totalCount,
            TBusinessObject[] data)
        {
            CurrentLimit = currentLimit;
            CurrentOffset = currentOffset;
            TotalCount = totalCount;
            Data = data;
        }

        public int CurrentLimit { get; }

        public int CurrentOffset { get; }

        public int TotalCount { get; }

        public TBusinessObject[] Data { get; }

        public static PaginationContainer<TBusinessObject> Empty()
        {
            return new PaginationContainer<TBusinessObject>(0, 0, 0, Array.Empty<TBusinessObject>());
        }
    }
}
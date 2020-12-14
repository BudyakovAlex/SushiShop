using SushiShop.Core.Data.Dtos.Pagination;
using SushiShop.Core.Data.Models.Pagination;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class PaginationContainerMapper
    {
        public static PaginationContainer<TBusinessObject> Map<TBusinessObject, TDto>(
            this PaginationContainerDto<TDto> paginationContainerDto,
            Func<TDto, TBusinessObject> transformFunc)
        {
            var dtos = paginationContainerDto!.Data ?? Array.Empty<TDto>();
            var businessObjects = dtos.Select(transformFunc).ToArray();

            return new PaginationContainer<TBusinessObject>(
                paginationContainerDto!.CurrentLimit,
                paginationContainerDto!.CurrentOffset,
                paginationContainerDto!.TotalCount,
                businessObjects);
        }
    }
}
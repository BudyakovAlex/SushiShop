using SushiShop.Core.Data.Dtos.Job;
using SushiShop.Core.Data.Models.Vacancy;

namespace SushiShop.Core.Mappers
{
    public static class VacancyMapper
    {
        public static Vacancy Map(this VacancyDto dto)
        {
            return new Vacancy(dto.Url, dto.Title, dto.Text);
        }
    }
}

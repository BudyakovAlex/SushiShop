using SushiShop.Core.Data.Dtos.Job;
using Model = SushiShop.Core.Data.Models.Vacancy;

namespace SushiShop.Core.Mappers.Vacancy
{
    public static class VacancyMapper
    {
        public static Model.Vacancy Map(this VacancyDto dto)
        {
            return new Model.Vacancy(dto.Url, dto.Title, dto.Text);
        }
    }
}
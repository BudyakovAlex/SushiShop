using System;

namespace SushiShop.Core.Data.Models.Common
{
    public class Content
    {
        public Content(
            int id,
            string? title,
            string? alias,
            string? mainText,
            DateTime? createdOn,
            DateTime? editedOn)
        {
            Id = id;
            Title = title;
            Alias = alias;
            MainText = mainText;
            CreatedOn = createdOn;
            EditedOn = editedOn;
        }

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Alias { get; set; }

        public string? MainText { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? EditedOn { get; set; }
    }
}
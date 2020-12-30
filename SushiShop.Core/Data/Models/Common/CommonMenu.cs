using System;

namespace SushiShop.Core.Data.Models.Common
{
    public class CommonMenu
    {
        public CommonMenu(
            long id,
            string? title,
            string? alias,
            ExtraData? extraData,
            DateTimeOffset createdOn,
            DateTimeOffset editedOn)
        {
            Id = id;
            Title = title;
            Alias = alias;
            ExtraData = extraData;
            CreatedOn = createdOn;
            EditedOn = editedOn;
        }

        public long Id { get; }

        public string? Title { get; }

        public string? Alias { get; }

        public ExtraData? ExtraData { get; }

        public DateTimeOffset CreatedOn { get; }

        public DateTimeOffset EditedOn { get; }
    }
}
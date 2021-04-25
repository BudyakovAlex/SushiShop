namespace SushiShop.Core.Common
{
    public static partial class Constants
    {
        public static class Format
        {
            public static class DateTime
            {
                public const string LongDate = "d MMMM yyyy";
                public const string ShortDate = "d MMMM";
                public const string DateWithTwentyFourTime = "d.MM.yyyy HH:mm";
                public const string DateOfBirth = "dd.MM.yyyy";
            }

            public static class Phone
            {
                public const char MaskCharacter = '#';

                public const string MaskFormat = "+# (###) ###-##-##";
            }
        }
    }
}

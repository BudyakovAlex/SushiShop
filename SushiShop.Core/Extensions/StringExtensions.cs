using SushiShop.Core.Common;
using System.Text;

namespace SushiShop.Core.Extensions
{
    public static class StringExtensions
    {
        public static string FilterByMask(this string source, string filter)
        {
            var onOriginal = 0;
            var onFilter = 0;
            var onOutput = 0;

            var outputString = new StringBuilder();
            var isCompleted = false;

            while (onFilter < filter.Length && !isCompleted)
            {
                var filterChar = filter[onFilter];
                var originalChar = onOriginal >= source.Length ? (char)0 : source[onOriginal];
                switch (filterChar)
                {
                    case Constants.Format.Phone.MaskCharacter:
                        if (originalChar == 0)
                        {
                            isCompleted = true;
                            break;
                        }

                        if (char.IsDigit(originalChar))
                        {
                            outputString.Append(originalChar);
                            onOriginal++;
                            onFilter++;
                            onOutput++;
                        }
                        else
                        {
                            onOriginal++;
                        }
                        break;
                    default:
                        // Any other character will automatically be inserted for the user as they type (spaces, - etc..) or deleted as they delete if there are more numbers to come.
                        outputString.Append(filterChar);
                        onOutput++;
                        onFilter++;

                        if (originalChar == filterChar)
                        {
                            onOriginal++;
                        }

                        break;
                }
            }

            return outputString.ToString();
        }
    }
}
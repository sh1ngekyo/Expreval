namespace Expreval.Core.Runtime.Extensions
{
    public static class StringExtensions
    {
        public static bool IsCorrectBrackets(this string input, char openedBracket, char closedBracket)
        {
            var delta = 0;
            foreach (var item in input)
            {
                if (item == openedBracket)
                    ++delta;
                else if (item == closedBracket)
                    --delta;
            }
            return delta == 0;
        }
    }
}

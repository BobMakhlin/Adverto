namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a tag.
    /// </summary>
    internal static class TagValidationOptions
    {
        /// <summary>
        /// Determines the minimum count of letters in a title.
        /// </summary>
        public static readonly int TitleMinLength = 4;
        /// <summary>
        /// Determines the maximum count of letters in a title.
        /// </summary>
        public static readonly int TitleMaxLength = 64;
    }
}
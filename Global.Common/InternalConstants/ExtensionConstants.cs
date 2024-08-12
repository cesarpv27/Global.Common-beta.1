namespace Global.Common.InternalConstants
{
    internal static class IDictionaryExtensionConstants
    {
        public static string NullDictionaryInJoin => "At least one of the elements in the dictionaries to join is null.";
    }

    internal static class EnumExtensionConstants
    {
        public static string IsNotValidEnum(string paramName)
        {
            return $"The generic parameter specified {paramName.WrapInSingleQuotationMarks()} is not a valid Enum";
        }
    }

    internal static class HelperConstants
    {
        public static string NotPropertiesFound => "No properties found";
    }
}

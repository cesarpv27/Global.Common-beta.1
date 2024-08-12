
namespace Global.Common.Enums
{
    /// <summary>
    /// Specifies the policy for attempting to create a resource.
    /// </summary>
    public enum TryCreateResourcePolicy
    {
        /// <summary>
        /// Tries to create the resource only the first time it is needed.
        /// </summary>
        OnlyFirstTime,

        /// <summary>
        /// Always tries to create the resource whenever it is needed.
        /// </summary>
        Always,

        /// <summary>
        /// Never tries to create the resource.
        /// </summary>
        Never
    }
}

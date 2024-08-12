
namespace Global.Common.Enums
{
    /// <summary>
    /// Specifies actions to take when a key already exists.
    /// </summary>
    public enum KeyExistAction
    {
        /// <summary>
        /// Attempts to add the key.
        /// </summary>
        TryAdd = 100,

        /// <summary>
        /// Updates the existing key.
        /// </summary>
        Update = 200,

        /// <summary>
        /// Renames the existing key.
        /// </summary>
        Rename = 300
    }
}

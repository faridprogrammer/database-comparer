using System.Data.Common;

namespace SecurityUtils
{
    /// <summary>
    /// Provides utilities for handling and masking sensitive information in database connection strings.
    /// </summary>
    public static class ConnectionStringMasker
    {
        /// <summary>
        /// Masks sensitive parts of a database connection string, such as passwords or secrets.
        /// </summary>
        /// <param name="connectionString">The original connection string.</param>
        /// <returns>A version of the connection string with sensitive values masked.</returns>
        public static string Mask(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            // Use DbConnectionStringBuilder to parse the string
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            var maskedBuilder = new DbConnectionStringBuilder();

            foreach (string key in builder.Keys)
            {
                var value = builder[key]?.ToString() ?? string.Empty;
                switch (key.ToLowerInvariant())
                {
                    case "password":
                    case "pwd":
                        maskedBuilder[key] = MaskValue(value);
                        break;
                    case "user id":
                    case "uid":
                        maskedBuilder[key] = MaskValue(value);
                        break;
                    default:
                        maskedBuilder[key] = value;
                        break;
                }
            }

            return maskedBuilder.ConnectionString;
        }

        /// <summary>
        /// Generates a masked version of the original value using asterisks.
        /// </summary>
        /// <param name="value">The original sensitive value.</param>
        /// <returns>A masked string with the same length as the original, or "***" if the original is empty.</returns>
        private static string MaskValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "***";

            return new string('*', value.Length);
        }
    }
}

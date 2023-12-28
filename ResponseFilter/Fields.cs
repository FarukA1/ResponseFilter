using System;

namespace ResponseFilter
{
    /// <summary>
    /// Represents a container for specifying fields to include in a filtered response.
    /// </summary>
    public class Fields
    {
        private string fields;

        /// <summary>
        /// Gets or sets the comma-separated list of fields to include in the filtered response.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when attempting to set 'ResponseFields' with a null or empty value.
        /// </exception>
        public string ResponseFields
        {
            get { return fields; }
            set
            {
                // Check if the value being set is not null or empty
                if (!string.IsNullOrEmpty(value))
                {
                    fields = value;
                }
                else
                {
                    // Handle the case where the value is null or empty
                    throw new ArgumentException("The 'ResponseFields' property cannot be null or empty.");
                }
            }
        }
    }
}

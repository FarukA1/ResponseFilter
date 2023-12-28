using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ResponseFilter
{
    /// <summary>
    /// Provides a generic filtering mechanism for extracting specific fields from an object.
    /// </summary>
    /// <typeparam name="T">The type of the object being filtered.</typeparam>
    public class Filter<T>
    {
        /// <summary>
        /// Gets the filtered response containing specified fields from the source object.
        /// </summary>
        /// <param name="data">The source object.</param>
        /// <param name="fields">The fields to include in the filtered response.</param>
        /// <returns>A JSON-formatted string containing the filtered fields.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the 'fields' parameter is null.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// Thrown when the 'ResponseFields' property of 'fields' is not set, or when a field has a null value in the source object.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a specified property is not found in the object.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs during field processing.
        /// </exception>
        public static object GetFilteredResponse(T data, Fields fields)
        {
            List<string> fieldList = ConvertToList(fields);

            ExpandoObject result = new ExpandoObject();

            foreach (var field in fieldList)
            {
                try
                {
                    var propertyInfo = data?.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo != null)
                    {
                        var fieldValue = propertyInfo.GetValue(data, null);

                        // Check if the field value is null and handle it appropriately
                        if (fieldValue == null)
                        {
                            throw new NullReferenceException($"{field} has a null value");
                        }

                        (result as IDictionary<string, object>).Add(field, fieldValue);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Property {field} not found in the object.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing field {field}: {ex.Message}", ex);
                }
            }

            var keyValuePairs = new Dictionary<string, object>();

            // Check if 'result' is an array or list of key-value pairs
            if (result is IEnumerable<KeyValuePair<string, object>> enumerable)
            {
                foreach (var item in enumerable)
                {
                    keyValuePairs.Add(item.Key, item.Value);
                }
            }

            // Convert the dictionary to JSON
            var jsonResult = JsonConvert.SerializeObject(keyValuePairs, Formatting.Indented);

            return jsonResult;
        }

        /// <summary>
        /// Converts the fields from the specified Fields object to a list of lowercase strings.
        /// </summary>
        /// <param name="fields">The Fields object containing the fields to be converted.</param>
        /// <returns>A list of lowercase strings representing the fields.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the 'fields' parameter is null.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// Thrown when the 'ResponseFields' property of 'fields' is not set.
        /// </exception>
        private static List<string> ConvertToList(Fields fields)
        {
            // Check if the 'fields' parameter is not null
            if (fields == null)
            {
                throw new ArgumentNullException(nameof(fields), "The 'fields' parameter cannot be null.");
            }

            // Check if ResponseFields property is not set
            if (fields.ResponseFields == null)
            {
                throw new NullReferenceException("The 'ResponseFields' property is not set.");
            }

            // Convert the ResponseFields to a list of lowercase strings
            var fieldList = fields.ResponseFields.ToLower().Split(',').ToList();

            return fieldList;
        }
    }
}

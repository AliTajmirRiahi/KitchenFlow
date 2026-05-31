using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Base.Core.ApiResults
{
    /// <summary>
    /// A unified wrapper for all API responses to ensure consistency.
    /// </summary>
    /// <typeparam name="T">The type of the data payload.</typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// Indicates whether the request was processed successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The actual data returned by the API.
        /// </summary>
        public T? Payload { get; set; }

        /// <summary>
        /// The UTC timestamp when the response was generated.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Total number of items available (used for paginated lists).
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Current page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// A collection of non-critical warning messages.
        /// </summary>
        public IEnumerable<string>? Warnings { get; set; }

        /// <summary>
        /// Creates a successful API result.
        /// </summary>
        /// <param name="payload">The data to return.</param>
        /// <param name="totalCount">Optional total record count for pagination.</param>
        /// <param name="page">Optional current page number.</param>
        /// <param name="pageSize">Optional page size.</param>
        /// <param name="warnings">Optional list of warnings.</param>
        /// <returns>A structured API result.</returns>
        public static ApiResult<T> Ok(
            T payload,
            int? totalCount = null,
            int? page = null,
            int? pageSize = null,
            IEnumerable<string>? warnings = null)
        {
            return new ApiResult<T>
            {
                Success = true,
                Payload = payload,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Warnings = warnings
            };
        }
    }
}


using EventDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDataAccess.ProcessorAPI.Data
{
    /// <summary>
    /// Create and Retrive information with context given in options for in memoery DB
    /// </summary>
    public class BatchContext : DbContext
    {
        public BatchContext(DbContextOptions<BatchContext> options): base(options) { }

        /// <summary>
        /// Data Set for Batch details.
        /// </summary>
        public DbSet<Batch> Batches { get; set; }
    }

}

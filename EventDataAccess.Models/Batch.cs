using System;
using System.ComponentModel.DataAnnotations;

namespace EventDataAccess.Models
{
    /// <summary>
    /// Model for Batch detail, for storing random num and multiply with random number
    /// </summary>
    public class Batch
    {
        [Key]
        public Guid ID { get; set; }
        public int OperationId { get; set; }
        public int BatchId { get; set; }
        public int ItemPosition { get; set; }
        public int GeneratedNum { get; set; }
        public DateTime GeneratedTime { get; set; }
        public int MultiplierNum { get; set; }
        public int MultipliedNum { get; set; }
        public DateTime MultipliedTime { get; set; }
    }
}

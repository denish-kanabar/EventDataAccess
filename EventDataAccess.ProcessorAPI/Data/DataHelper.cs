using EventDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EventDataAccess.ProcessorAPI.Data
{
    public class DataHelper
    {
        /// <summary>
        /// Stores information in memory 
        /// </summary>
        /// <param name="input">Object with number of batches and numbers in each batch to process</param>
        /// <returns></returns>
        internal static List<Batch> AddBatches(RequestInput input)
        {
            List<Batch> batches = new();

            for (int batchCount = 1; batchCount <= input.BatchCount; batchCount++)
            {
                for (int nosInBatch = 1; nosInBatch <= input.NumberCount; nosInBatch++)
                {
                    batches.Add(new Batch { ID = new Guid(), BatchId = batchCount, OperationId = input.OperationId, ItemPosition = nosInBatch });
                }
            }
            StoreBatchDetails(batches);

            return batches;
        }

        /// <summary>
        /// Update batches stored in memory
        /// </summary>
        /// <param name="batches">Batch details with records for each number</param>
        internal static void StoreBatchDetails(IEnumerable<Batch> batches)
        {
            var options = new DbContextOptionsBuilder<BatchContext>()
                                  .UseInMemoryDatabase(databaseName: "BatchNumber").Options;

            using var context = new BatchContext(options);
            context.Batches.UpdateRange(batches);
            context.SaveChanges();           
        }
    }
}

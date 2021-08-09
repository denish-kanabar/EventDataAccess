using EventDataAccess.Models;
using EventDataAccess.ProcessorAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventDataAccess.ProcessorAPI.Managers
{
    public class AggregatorManager
    {
        /// <summary>
        /// Provides list of batches and each batch contiesn list for generated and multiplied number with required details
        /// </summary>
        /// <param name="operationId">Identifier for current and previous process / execution</param>
        /// <returns>Batches for given operation </returns>
        internal static IEnumerable<IEnumerable<Batch>> GetcurrentBatchDetail(int operationId)
        {
            try
            {
                if (operationId < 0)
                    return null;
                else
                {
                    var options = new DbContextOptionsBuilder<BatchContext>()
                                         .UseInMemoryDatabase(databaseName: "BatchNumber").Options;

                    var resultData = new BatchContext(options).Batches.ToList();

                    return resultData.Where(a => a.OperationId == operationId).GroupBy(b => b.BatchId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

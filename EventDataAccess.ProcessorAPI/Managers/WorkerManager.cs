using EventDataAccess.Models;
using EventDataAccess.ProcessorAPI.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDataAccess.ProcessorAPI.Managers
{
    public class WorkerManager
    {
        /// <summary>
        /// To Start Processing input request in parallel for all batches , generation and next multiplication 
        /// </summary>
        /// <param name="batchList">Batches to generate and multiply with random numbers</param>
        /// <param name="input">Request received from User action with number of batches and numbers in each batch</param>
        internal static void StartManager(IEnumerable<Batch> batchList, RequestInput input)
        {

            if ((Enumerable.Range(1, 10).Contains(input.BatchCount) && Enumerable.Range(1, 10).Contains(input.NumberCount)))
            {
                Parallel.For(1, input.BatchCount + 1, async i =>
                {
                    Task<IEnumerable<Batch>> taskNumberGeneration = Task.Run(async () =>
                    {
                        var generatedList = await Execute(batchList.Where(x => x.BatchId == i), WorkType.GENERATE);
                        DataHelper.StoreBatchDetails(generatedList);
                        return generatedList;
                    });

                    // Execute the continuation when the antecedent finishes.
                    await taskNumberGeneration.ContinueWith(async antecedent =>
                    {
                    var resultList = await Execute(antecedent.Result.Where(y => y.BatchId == i), WorkType.MULTIPLY);
                    DataHelper.StoreBatchDetails(resultList);
                    });
                });
            }
        }

        /// <summary>
        /// Defines worker ApI call for random number genration and multiplication
        /// </summary>
        /// <param name="batches">Batche to generate or multiply with number</param>
        /// <param name="workType">Enum of Generate or Multiply</param>
        /// <returns></returns>
        private static async Task<IEnumerable<Batch>> Execute(IEnumerable<Batch> batches, WorkType workType)
        {
            WorkerRequest workerRequest = new() { Batches = batches.ToList(), RequestWorkType = workType };

            RestClient client = new("https://localhost:44381/");
            var request = new RestRequest("Worker").AddJsonBody(workerRequest);

            List<Batch> result = await client.PostAsync<List<Batch>>(request);

            return result;
        }
    }
}

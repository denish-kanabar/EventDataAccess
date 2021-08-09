using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using EventDataAccess.Models;

namespace EvenDataAccess.WorkerAPI.Controllers.Tests
{
    [TestClass()]
    public class WorkerControllerTests
    {
        [TestMethod()]
        public void WorkerTestBlankList()
        {
            List<Batch> batches = new();
            WorkerRequest workerRequest = new() { Batches = batches, RequestWorkType = WorkType.GENERATE };
            WorkerStrategy workerStrategy = new();
            var result = workerStrategy.WorkExecutor(workerRequest.Batches, workerRequest.RequestWorkType).ToList();

            //Should return blank list if blank list is given for processing
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void WorkerTestWithList()
        {
            List<Batch> batches = new();
            int inputNumber = 2;
            for (int batchCount = 1; batchCount <= inputNumber; batchCount++)
            {
                for (int nosInBatch = 1; nosInBatch <= inputNumber; nosInBatch++)
                {
                    batches.Add(new Batch { ID = new Guid(), BatchId = batchCount, OperationId = 1, ItemPosition = nosInBatch });
                }
            }
           
            WorkerRequest workerRequest = new() { Batches = batches, RequestWorkType = WorkType.GENERATE };
            WorkerStrategy workerStrategy = new();
            var result = workerStrategy.WorkExecutor(workerRequest.Batches, workerRequest.RequestWorkType).ToList();
            
            //Should return list with records if valid list is given for processing. Result will be 4 here as no of batches and count in each is 2
            Assert.IsTrue(result.Count == inputNumber* inputNumber);
        }

        [TestMethod()]
        public void WorkerTestWithGenerateNumber()
        {
            List<Batch> batches = new();
            int inputNumber = 2;
            for (int batchCount = 1; batchCount <= inputNumber; batchCount++)
            {
                for (int nosInBatch = 1; nosInBatch <= inputNumber; nosInBatch++)
                {
                    batches.Add(new Batch { ID = new Guid(), BatchId = batchCount, OperationId = 1, ItemPosition = nosInBatch });
                }
            }

            WorkerRequest workerRequest = new() { Batches = batches, RequestWorkType = WorkType.GENERATE };
            WorkerStrategy workerStrategy = new();
            var result = workerStrategy.WorkExecutor(workerRequest.Batches, workerRequest.RequestWorkType).ToList();

            //Should return null as all records should have generaed raddom number in 1, 100 range
            Assert.IsNull(result.Find(x => x.GeneratedNum == 0));
        }

        [TestMethod()]
        public void WorkerTestWithMultiplyNumber()
        {
            List<Batch> batches = new();
            int inputNumber = 2;
            Random random = new();
            for (int batchCount = 1; batchCount <= inputNumber; batchCount++)
            {
                for (int nosInBatch = 1; nosInBatch <= inputNumber; nosInBatch++)
                {
                    batches.Add(new Batch { ID = new Guid(), BatchId = batchCount, OperationId = 1, ItemPosition = nosInBatch, GeneratedNum= random.Next(1,100) });
                }
            }

            WorkerRequest workerRequest = new() { Batches = batches, RequestWorkType = WorkType.MULTIPLY };
            WorkerStrategy workerStrategy = new();
            var result = workerStrategy.WorkExecutor(workerRequest.Batches, workerRequest.RequestWorkType).ToList();

            //Should return null as all records should have Multiplied raddom number in 1, 100 range
            Assert.IsNull(result.Find(x => x.MultipliedNum == 0));
        }

    }
}
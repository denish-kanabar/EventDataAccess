using EvenDataAccess.WorkerAPI;
using EventDataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventDataAccess.UnitTests.Controllers.Tests
{
    [TestClass()]
    public class GeneratorStrategyTests
    {

        [TestMethod()]
        public void ExecuteTestWithBlankList()
        {
            List<Batch> batches = new();

            GeneratorStrategy generatorStrategy = new();
            var result = generatorStrategy.Execute(batches).ToList();

            Console.WriteLine(result.Count);

            //Should return blank list if blank list is given for processing
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void ExecuteTestWithValueList()
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
            GeneratorStrategy generatorStrategy = new();
            var result = generatorStrategy.Execute(batches).ToList();

            //Should return null as all records should have generaed raddom number in 1, 100 range
            Assert.IsNull(result.Find(x => x.GeneratedNum == 0));
        }
    }
}

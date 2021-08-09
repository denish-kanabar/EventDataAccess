using EventDataAccess.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using RequestInput = EventDataAccess.Models.RequestInput;

namespace EventDataAccess.ProcessorAPI.Controllers.Tests
{
    [TestClass()]
    public class ProcessorControllerTests
    {
        readonly ILogger<ProcessorController> logger;
        public ProcessorControllerTests()
        {

            var mock = new Mock<ILogger<ProcessorController>>();
            logger = mock.Object;
            logger = Mock.Of<ILogger<ProcessorController>>();
        }

        [DataTestMethod]
        [DataRow("-1")]
        public void GetAggregatorDetailTestNullCheck(string operationId)
        {

            ProcessorController processorController = new(logger);
            var batches = processorController.GetAggregatorDetail(operationId);

            //should return null in case of negative value
            Assert.AreEqual(null, batches);
        }

        [DataTestMethod]
        [DataRow("1")]
        public void GetAggregatorDetailTestValidInputCheck(string operationId)
        {
            ProcessorController processorController = new(logger);
            var batches = processorController.GetAggregatorDetail(operationId);

            //should return not null in case of positive value
            Assert.AreNotEqual(null, batches);
        }

        [DataTestMethod()]
        [DataRow(2, 3, 1)]
        public void StartProcessingTest(int batchCountInput, int numberCountInput, int opertionId)
        {
            RequestInput input = new() { BatchCount = batchCountInput, NumberCount = numberCountInput, OperationId = opertionId };

            ProcessorController processorController = new(logger);

            processorController.StartProcessing(input);

            IEnumerable<IEnumerable<Batch>> batches = processorController.GetAggregatorDetail("1");
            var result = batches.Select(i => i.ToList()).ToList();
            
            //List with more than one item for batches of given range
            Assert.IsTrue(result.Count > 0);
        }

        [DataTestMethod()]
        [DataRow(-2, -3, -1)]
        public void StartProcessingNoRecordCheck(int batchCountInput, int numberCountInput, int opertionId)
        {
            RequestInput input = new() { BatchCount = batchCountInput, NumberCount = numberCountInput, OperationId = opertionId };

            ProcessorController processorController = new(logger);

            processorController.StartProcessing(input);

            IEnumerable<IEnumerable<Batch>> batches = processorController.GetAggregatorDetail("-1");
            Assert.AreEqual(null, batches);
        }
    }
}
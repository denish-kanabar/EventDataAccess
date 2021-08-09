using EventDataAccess.Models;
using EventDataAccess.ProcessorAPI.Data;
using EventDataAccess.ProcessorAPI.Managers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EventDataAccess.ProcessorAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ProcessorController
    {

        private readonly ILogger<ProcessorController> _logger;

        public ProcessorController(ILogger<ProcessorController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [Route("startProcess")]
        public void StartProcessing([FromBody] RequestInput input)
        {
            _logger.LogInformation($"Processing request for {input.BatchCount} batches {input.NumberCount} numbers in each batch " +
                $"to generate random and multiply with random numbers.");
            WorkerManager.StartManager(DataHelper.AddBatches(input), input);
        }

        [HttpGet]
        [Route("getAggregator")]
        public IEnumerable<IEnumerable<Batch>> GetAggregatorDetail([FromQuery] string operationId)
        {
            //_logger.LogInformation("Received request to provide generated numbers detail store in inMemory DB.");
            return AggregatorManager.GetcurrentBatchDetail(Convert.ToInt32(operationId));
        }
    }
}

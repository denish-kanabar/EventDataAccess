using System.Collections.Generic;
using EventDataAccess.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EvenDataAccess.WorkerAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class WorkerController
    {
        private readonly ILogger<WorkerController> _logger;
        public WorkerController(ILogger<WorkerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Worker")]
        public List<Batch> Worker(WorkerRequest workerRequest)
        {
           // _logger.LogInformation("Received request to provide generated & multiply numbers detail store in inMemory DB.");
            WorkerStrategy workerStrategy = new();
            var result = workerStrategy.WorkExecutor(workerRequest.Batches, workerRequest.RequestWorkType).ToList();
            return result;
        }
    }
}

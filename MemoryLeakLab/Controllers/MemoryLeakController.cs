using MemoryLeakLab.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MemoryLeakLab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoryLeakController : ControllerBase
    {
        [HttpPost("leak")]
        public IActionResult CreateLeak([FromQuery] int count = 100)
        {
            for (var i = 0; i < count; i++)
            {
                _ = new LeakySubscriber();
            }

            return Ok(new
            {
                created = count,
                subscribers = StaticEventSource.SubscriberCount
            });
        }

        [HttpPost("fixed")]
        public IActionResult CreateFixed([FromQuery] int count = 100)
        {
            for (var i = 0; i < count; i++)
            {
                using var subscriber = new FixedSubscriber();
            }

            return Ok(new
            {
                created = count,
                subscribers = StaticEventSource.SubscriberCount
            });
        }

        [HttpPost("gc")]
        public IActionResult ForceGc()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return Ok(GetStats());
        }

        [HttpGet("stats")]
        public IActionResult Stats()
        {
            return Ok(GetStats());
        }

        private static object GetStats()
        {
            var process = Process.GetCurrentProcess();

            return new
            {
                subscribers = StaticEventSource.SubscriberCount,
                managedMemoryMb = Math.Round(
                    GC.GetTotalMemory(false) / 1024d / 1024d,
                    2),
                workingSetMb = Math.Round(
                    process.WorkingSet64 / 1024d / 1024d,
                    2)
            };
        }
    }
}
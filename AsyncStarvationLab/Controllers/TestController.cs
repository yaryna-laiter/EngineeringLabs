using Microsoft.AspNetCore.Mvc;

namespace EngineeringLabs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    private async Task<string> TestAsyncMethod(string message)
    {
        await Task.Delay(1000);
        
        return message;
    }
    
    [HttpGet("good")]
    public async Task<string> GoodAsyncMethod()
    {
        return await TestAsyncMethod("Good request completed");
    }

    [HttpGet("bad")]
    public async Task<string> BadasyncMethod()
    {
        return TestAsyncMethod("Bad request completed").Result;
    }
}
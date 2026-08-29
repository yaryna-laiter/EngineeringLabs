using Microsoft.AspNetCore.Mvc;

namespace EngineeringLabs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    private int goodCounter = 0;
    private int badCounter = 0;
    private async Task<string> TestAsyncMethod(string message)
    {
        await Task.Delay(1000);
        
        return message;
    }
    
    [HttpGet("good")]
    public async Task<string> GoodAsyncMethod()
    {
        return await TestAsyncMethod($"Request g {++goodCounter}");
    }

    [HttpGet("bad")]
    public async Task<string> BadasyncMethod()
    {
        return TestAsyncMethod($"Request b {badCounter}").Result;
    }
}
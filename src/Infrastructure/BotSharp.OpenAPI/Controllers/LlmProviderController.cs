using BotSharp.Abstraction.MLTasks;
using BotSharp.Abstraction.MLTasks.Settings;

namespace BotSharp.OpenAPI.Controllers;

[Authorize]
[ApiController]
public class LlmProviderController : ControllerBase
{
    private readonly IServiceProvider _services;
    private readonly ILlmProviderService _llmProvider;
    public LlmProviderController(IServiceProvider services, ILlmProviderService llmProvider)
    {
        _services = services;
        _llmProvider = llmProvider;
    }

    [HttpGet("/llm-providers")]
    public IEnumerable<string> GetLlmProviders()
    {
        return _llmProvider.GetProviders();
    }

    [HttpGet("/llm-provider/{provider}/models")]
    public IEnumerable<LlmModelSetting> GetLlmProviderModels([FromRoute] string provider)
    {
        var list = _llmProvider.GetProviderModels(provider);
        return list.Where(x => x.Type == LlmModelType.Chat);
    }

    [HttpGet("/llm-configs")]
    public List<LlmProviderSetting> GetLlmConfigs([FromQuery] LlmConfigOptions options)
    {
        var configs = _llmProvider.GetLlmConfigs(options);
        return configs;
    }
}

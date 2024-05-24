using Azure.Identity;
using Microsoft.SemanticKernel;

var ModelName = Environment.GetEnvironmentVariable("MODEL_NAME")!;
var Endpoint = Environment.GetEnvironmentVariable("OPEN_AI_ENDPOINT")!;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    ModelName,
    Endpoint,
    new DefaultAzureCredential());

var kernel = builder.Build();

var prompt = """
あなたは誰ですか？
""";
// 実行して結果を表示
var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result.GetValue<string>());

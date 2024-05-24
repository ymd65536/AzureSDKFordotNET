using Azure.Identity;
using Microsoft.SemanticKernel;
using System.ComponentModel;

var ModelName = Environment.GetEnvironmentVariable("MODEL_NAME")!;
var Endpoint = Environment.GetEnvironmentVariable("OPEN_AI_ENDPOINT")!;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    ModelName,
    Endpoint,
    new DefaultAzureCredential());

var kernel = builder.Build();

var prompt = """
    今日の日付を教えてください。

    ## 参考情報
    {{ CreateListItemPlugin.CreateItem categoryName='今日の日付' value='2023/12/25' }}
    """;

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result.GetValue<string>());

class CreateListItemPlugin
{
    [KernelFunction]
    [Description("リストの1項目を生成します。")]
    public string CreateItem(
        [Description("カテゴリ名")]
        string categoryName,
        [Description("カテゴリの値")]
        string value) =>
        $"- {categoryName}: {value}";
}
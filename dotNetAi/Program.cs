using dotNetAi.Service;
using DotNetAiErudio.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddOpenAI();

builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddSingleton<ImageService>();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
	builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader();
}));

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
	options.AddDocumentTransformer((document, context, _) =>
	{
		document.Info = new()
		{
			Title = ".NET AI hmarcone API",
			Version = "v1",
			Description = """  
               This API provides AI-based features such as chat, image generation,  
               recipe creation and audio transcription.  
               """,
			Contact = new()
			{
				Name = "hmarcone Training",
				Email = "hmarcone@gmail.com",
				Url = new Uri("https://hmarcone.com.br/meus-cursos")
			},
			License = new()
			{
				Name = "Apache 2 License",
				Url = new Uri("https://hmarcone.com.br/meus-cursos")
			},
			TermsOfService = new Uri("https://hmarcone.com.br/meus-cursos")
		};
		return Task.CompletedTask;
	});
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference(options =>
	{
		options.Title = ".NET AI hmarcone API";
		options.Theme = ScalarTheme.Default;
		options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

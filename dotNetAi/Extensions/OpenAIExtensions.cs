using OpenAI;

namespace DotNetAiErudio.Extensions
{
    public static class OpenAIExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder)
        {
            //var apiKey = builder.Configuration["OpenAI:Key"];
            var apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");

			// Tenta buscar especificamente nas variáveis do Usuário
			var key = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY", EnvironmentVariableTarget.User);

			// Ou nas variáveis da Máquina (exige admin para criar, mas o código lê normal)
			var keyMachine = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY", EnvironmentVariableTarget.Machine);

			if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            var openAIClient = new OpenAIClient(apiKey);

            builder.Services.AddSingleton(openAIClient);
            return builder;
        }
    }
}

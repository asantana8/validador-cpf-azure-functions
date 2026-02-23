using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace br.com.asantana.functions
{
    public class FnValidaCPF
    {
        private readonly ILogger _logger;

        public FnValidaCPF(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FnValidaCPF>();
        }

        [Function("ValidaCPF")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processando validação de CPF...");

            // 1. Ler o corpo da requisição
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            // 2. Configurar a desserialização para ignorar maiúsculas/minúsculas
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<CpfRequest>(requestBody, options);

            // 3. Validação de entrada
            if (data == null || string.IsNullOrEmpty(data.Cpf))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteAsJsonAsync(new { mensagem = "Por favor, informe o CPF no corpo da requisição." });
                return badResponse;
            }

            // 4. Lógica de Negócio
            bool isValid = ValidarCpf(data.Cpf);
            
            // 5. Resposta do Microsserviço
            var response = req.CreateResponse(isValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(new 
            { 
                valido = isValid, 
                mensagem = isValid ? "CPF válido." : "CPF inválido.",
                data_processamento = DateTime.Now
            });

            return response;
        }

        private bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // CPF deve ter 11 números e não pode ter todos os dígitos iguais
            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf) return false;

            // Algoritmo simplificado de validação de dígitos (Placeholder para sua lógica)
            // Como arquiteto, você pode inserir aqui a validação real de módulo 11
            return true; 
        }
    }

    // Classe de modelo para o JSON de entrada
    public class CpfRequest
    {
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }
    }
}
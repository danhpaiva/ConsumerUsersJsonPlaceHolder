using System.Text.Json;
using ConsumerUsersJsonPlaceHolder.Models;

namespace ConsumerUsersJsonPlaceHolder.Client;

public class Client
{
    private static readonly HttpClient _httpClient = new();
    private const string ApiUrl = "https://jsonplaceholder.typicode.com/users";

    public static async Task ConsumirApi()
    {
        Console.WriteLine("Iniciando consumo da API de usuários...");

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<User>? users = JsonSerializer
                .Deserialize<List<User>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (users != null && users.Any())
            {
                var firstUser = users.FirstOrDefault();

                if (firstUser != null)
                {
                    Console.WriteLine("\nPrimeiro usuário encontrado:");
                    Console.WriteLine("---------------------");

                    Console.WriteLine($"ID: {firstUser.Id}");
                    Console.WriteLine($"Nome: {firstUser.Name}");
                    Console.WriteLine($"Usuário: {firstUser.Username}");
                    Console.WriteLine($"Email: {firstUser.Email}");
                    Console.WriteLine($"Endereço: {firstUser.Address.Street}, {firstUser.Address.Suite}, {firstUser.Address.City}, {firstUser.Address.Zipcode}");
                    Console.WriteLine($"Empresa: {firstUser.Company.Name}");
                    Console.WriteLine("---------------------");
                }
                else
                {
                    Console.WriteLine("Nenhum usuário foi retornado da API ou a lista está vazia.");
                }
            }
            else
            {
                Console.WriteLine("Nenhum usuário foi retornado da API ou a lista está vazia.");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"\nErro de requisição HTTP: {e.Message}");
            Console.WriteLine($"Verifique a URL ou sua conexão com a internet.");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"\nErro de desserialização JSON: {e.Message}");
            Console.WriteLine($"A estrutura do JSON pode estar diferente do esperado.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nOcorreu um erro inesperado: {e.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}

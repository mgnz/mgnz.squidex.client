namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Transport;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive",
    "Content-Type: application/json", "Pragma: no-cache", "Accept: application/json")]
  public interface ISquidexAppClient
  {
    [Post("/api/apps/")]
    Task<dynamic> CreateApp(CreateAppRequest request);

    [Get("/api/apps/")]
    Task<dynamic> GetAllApps();

    [Delete("/api/apps/{app}/")]
    Task<dynamic> DeleteApp(string app);

    [Post("/api/apps/{app}/clients")]
    Task<dynamic> CreateClient(string app, CreateClientRequest request);

    [Get("/api/apps/{app}/clients/")]
    Task<dynamic> GetAllClients(string app);

    [Put("/api/apps/{app}/clients/{client}/")]
    Task<dynamic> UpdateClient(string app, string client, UpdateClientRequest request);
  }
}
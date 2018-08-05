namespace MGNZ.Squidex.Client.Handlers
{
  using System;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading;
  using System.Threading.Tasks;

  public class SimpleAccessTokenHttpClientHandler : HttpClientHandler
  {
    private readonly Func<Task<string>> getToken;

    public SimpleAccessTokenHttpClientHandler(Func<Task<string>> getToken)
    {
      if (getToken == null) throw new ArgumentNullException(nameof(getToken));

      this.getToken = getToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      var authorizationHeadder = request.Headers.Authorization;
      if (authorizationHeadder != null)
      {
        var token = await this.getToken().ConfigureAwait(false);

        request.Headers.Authorization = new AuthenticationHeaderValue(authorizationHeadder.Scheme, token);
      }

      return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
  }
}
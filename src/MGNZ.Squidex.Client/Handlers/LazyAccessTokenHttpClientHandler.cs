namespace MGNZ.Squidex.Client.Handlers
{
  using System;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading;
  using System.Threading.Tasks;

  public class LazyAccessTokenHttpClientHandler : HttpClientHandler
  {
    private readonly Lazy<Func<Task<string>>> _tokenHandler;

    public LazyAccessTokenHttpClientHandler(Func<Task<string>> tokenHandler)
    {
      if (tokenHandler == null) throw new ArgumentNullException(nameof(tokenHandler));

      this._tokenHandler = new Lazy<Func<Task<string>>>(() => tokenHandler);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      var authorizationHeadder = request.Headers.Authorization;
      if (authorizationHeadder != null)
      {
        var token = await this.GetToken().ConfigureAwait(false);

        request.Headers.Authorization = new AuthenticationHeaderValue(authorizationHeadder.Scheme, token);
      }

      return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private async Task<string> GetToken()
    {
      // todo : log the fact we are getting the token (info)

      var token = await this._tokenHandler.Value().ConfigureAwait(false);

      return token;
    }
  }
}
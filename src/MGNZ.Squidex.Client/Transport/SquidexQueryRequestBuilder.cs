namespace MGNZ.Squidex.Client.Model.Transport
{
  using System;

  public class SquidexQueryRequestBuilder
  {
    public static SquidexQueryRequest Build(string requestedPage = "all", string requestedOrderBy = null, string requestedFilter = null, string requestedSearch = null)
    {
      var request = new SquidexQueryRequest();

      request.OrderBy = requestedOrderBy;

      if((string.IsNullOrWhiteSpace(requestedFilter) || string.IsNullOrEmpty(requestedFilter) == false) && (string.IsNullOrWhiteSpace(requestedSearch) || string.IsNullOrEmpty(requestedSearch)) == false)
        throw new ArgumentException($"{nameof(requestedFilter)} and {nameof(requestedSearch)} are mutually exclusive; please provide either a {nameof(requestedFilter)} or a {nameof(requestedSearch)}");

      request.Filter = requestedFilter;
      request.Search = requestedSearch;

      switch (requestedPage.ToLowerInvariant().Trim())
      {
        case "all":
          request.Top = 200;
          request.Skip = 0;
          break;
        default:
        {
          if (int.TryParse(requestedPage, out var parsedPage))
          {
            if (parsedPage == 1)
            {
              request.Top = 20;
              request.Skip = 0;
            }
            else
            {
              request.Top = 20;
              request.Skip = (parsedPage - 1) * 20;
            }
          }
          else
            throw new ArgumentOutOfRangeException(nameof(requestedPage), requestedPage,
              $"Found '{requestedPage}', expected values; either 'all' or an integer");

          break;
        }
      }

      return request;
    }
  }
}
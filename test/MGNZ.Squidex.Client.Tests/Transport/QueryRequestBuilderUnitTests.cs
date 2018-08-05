namespace MGNZ.Squidex.Client.Tests.Transport
{
  using System;
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Transport;

  using Xunit;

  [Trait("category", "unit")]
  public class QueryRequestBuilderUnitTests
  {
    public static IEnumerable<object[]> Build_HappyPath_Data => new List<object[]>
    {
      new object[] { "a", new QueryRequest() { Top = 20, Skip = 0, Filter = null, Search = null, OrderBy = null }, "1", null, null, null },
      new object[] { "b", new QueryRequest() { Top = 20, Skip = 20, Filter = null, Search = null, OrderBy = null }, "2", null, null, null },
      new object[] { "c", new QueryRequest() { Top = 20, Skip = 40, Filter = null, Search = null, OrderBy = null }, "3", null, null, null },
      new object[] { "d", new QueryRequest() { Top = 20, Skip = 60, Filter = null, Search = null, OrderBy = null }, "4", null, null, null },
      new object[] { "e", new QueryRequest() { Top = 20, Skip = 80, Filter = null, Search = null, OrderBy = null }, "5", null, null, null },
      new object[] { "f", new QueryRequest() { Top = 20, Skip = 100, Filter = null, Search = null, OrderBy = null }, "6", null, null, null },
      new object[] { "g", new QueryRequest() { Top = 20, Skip = 120, Filter = null, Search = null, OrderBy = null }, "7", null, null, null },
      new object[] { "h", new QueryRequest() { Top = 20, Skip = 140, Filter = null, Search = null, OrderBy = null }, "8", null, null, null },
      new object[] { "i", new QueryRequest() { Top = 20, Skip = 160, Filter = null, Search = null, OrderBy = null }, "9", null, null, null },
      new object[] { "j", new QueryRequest() { Top = 20, Skip = 180, Filter = null, Search = null, OrderBy = null }, "10", null, null, null },
      new object[] { "l", new QueryRequest() { Top = 200, Skip = 0, Filter = null, Search = null, OrderBy = null }, "all", null, null, null },
      new object[] { "m", new QueryRequest() { Top = 200, Skip = 0, Filter = "filterbythis", Search = "", OrderBy = "orderbythis" }, "all", "orderbythis", "filterbythis", "" },
      new object[] { "o", new QueryRequest() { Top = 20, Skip = 0, Filter = "filterbythis", Search = "", OrderBy = null }, "1", null, "filterbythis", ""},
      new object[] { "q", new QueryRequest() { Top = 20, Skip = 80, Filter = "filterbythis", Search = "", OrderBy = "orderbythis" }, "5", "orderbythis", "filterbythis", ""},
      new object[] { "r", new QueryRequest() { Top = 20, Skip = 160, Filter = "filterbythis", Search = "", OrderBy = "orderbythis" }, "9", "orderbythis", "filterbythis", "" },
    };

    [Theory]
    [MemberData(nameof(Build_HappyPath_Data))]
    public void Build_HappyPath(string testCase, QueryRequest expected, string requestedPageIn, string requestedOrderByIn, string requestedFilterIn, string requestedSearchIn)
    {
      var actual = QueryRequestBuilder.Build(requestedPageIn, requestedOrderByIn, requestedFilterIn, requestedSearchIn);

      actual.Top.Should().Be(expected.Top, $"where id is [{testCase}]");
      actual.Skip.Should().Be(expected.Skip, $"where id is [{testCase}]");
      actual.Filter.Should().Be(expected.Filter, $"where id is [{testCase}]");
      actual.Search.Should().Be(expected.Search, $"where id is [{testCase}]");
      actual.OrderBy.Should().Be(expected.OrderBy, $"where id is [{testCase}]");
    }

    public static IEnumerable<object[]> Build_SadPath_Data => new List<object[]>
    {
      new object[] { "n", new ArgumentException(), "all", "orderbythis", "", "searchbythis" },
      new object[] { "p", new ArgumentException(), "3", "orderbythis", "", "searchbythis" }
    };

    [Theory]
    [MemberData(nameof(Build_SadPath_Data))]
    public void Build_SadPath(string testCase, Exception expectedException, string requestedPageIn, string requestedOrderByIn, string requestedFilterIn, string requestedSearchIn)
    {
      Assert.Throws(expectedException.GetType(), () => QueryRequestBuilder
        .Build(requestedPageIn, requestedOrderByIn, requestedFilterIn, requestedSearchIn));
    }
  }
}
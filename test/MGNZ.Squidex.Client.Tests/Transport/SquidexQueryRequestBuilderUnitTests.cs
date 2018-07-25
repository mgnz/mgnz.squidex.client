namespace MGNZ.Squidex.Client.Tests.Transport
{
  using System;
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;

  using Xunit;

  [Trait("category", "unit")]
  public class SquidexQueryRequestBuilderUnitTests
  {
    public static IEnumerable<object[]> Build_HappyPath_Data => new List<object[]>
    {
      new object[] { new SquidexQueryRequest() { Top = 20, Skip = 0, Filter = null, Search = null, OrderBy = null }, "1", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 40, Skip = 20, Filter = null, Search = null, OrderBy = null }, "2", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 60, Skip = 40, Filter = null, Search = null, OrderBy = null }, "3", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 80, Skip = 60, Filter = null, Search = null, OrderBy = null }, "4", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 100, Skip = 80, Filter = null, Search = null, OrderBy = null }, "5", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 120, Skip = 100, Filter = null, Search = null, OrderBy = null }, "6", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 140, Skip = 120, Filter = null, Search = null, OrderBy = null }, "7", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 160, Skip = 140, Filter = null, Search = null, OrderBy = null }, "8", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 180, Skip = 160, Filter = null, Search = null, OrderBy = null }, "9", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 200, Skip = 180, Filter = null, Search = null, OrderBy = null }, "10", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 220, Skip = 200, Filter = null, Search = null, OrderBy = null }, "10", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 200, Skip = 0, Filter = null, Search = null, OrderBy = null }, "all", null, null, null },
      new object[] { new SquidexQueryRequest() { Top = 200, Skip = 0, Filter = "filterbythis", Search = null, OrderBy = "orderbythis" }, "all", "orderbythis", "filterbythis", "" },
      new object[] { new SquidexQueryRequest() { Top = 200, Skip = 0, Filter = null, Search = "searchbythis", OrderBy = "orderbythis" }, "all", "orderbythis", "", "searchbythis" },
      new object[] { new SquidexQueryRequest() { Top = 20, Skip = 0, Filter = "filterbythis", Search = null, OrderBy = null }, "1", null, "filterbythis", ""},
      new object[] { new SquidexQueryRequest() { Top = 60, Skip = 40, Filter = null, Search = "searchbythis", OrderBy = "orderbythis" }, "3", "orderbythis", "", "searchbythis" },
      new object[] { new SquidexQueryRequest() { Top = 100, Skip = 80, Filter = "filterbythis", Search = null, OrderBy = "orderbythis" }, "5", "orderbythis", "filterbythis", ""},
      new object[] { new SquidexQueryRequest() { Top = 140, Skip = 120, Filter = null, Search = "searchbythis", OrderBy = null }, "7", null, "", "searchbythis" },
      new object[] { new SquidexQueryRequest() { Top = 180, Skip = 160, Filter = "filterbythis", Search = null, OrderBy = "orderbythis" }, "9", "orderbythis", "filterbythis", "" },
    };

    [Theory]
    [MemberData(nameof(Build_HappyPath_Data))]
    public void Build_HappyPath(SquidexQueryRequest expected, string requestedPageIn, string requestedOrderByIn, string requestedFilterIn, string requestedSearchIn)
    {
      var actual = SquidexQueryRequestBuilder.Build(requestedFilterIn, requestedOrderByIn, requestedFilterIn, requestedSearchIn);

      actual.Top.Should().Be(expected.Top);
      actual.Skip.Should().Be(expected.Skip);
      actual.Filter.Should().Be(expected.Filter);
      actual.Search.Should().Be(expected.Search);
      actual.OrderBy.Should().Be(expected.OrderBy);
    }

    public static IEnumerable<object[]> Build_SadPath_Data => new List<object[]>
    {
      new object[] { new Exception() {}, "", "", "", "" }
    };

    [Theory]
    [MemberData(nameof(Build_SadPath_Data))]
    public void Build_SadPath(Exception expected, string requestedPageIn, string requestedOrderByIn, string requestedFilterIn, string requestedSearchIn)
    {

    }
  }
}
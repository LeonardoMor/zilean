using Zilean.Shared.Features.Utilities;

namespace Zilean.Tests.Tests;

public class ParsingExtractSeasonEpisodeTests
{
    [Theory]
    [InlineData("NYPD Blue S04", "NYPD Blue", 4, null)]
    [InlineData("NYPD Blue s06", "NYPD Blue", 6, null)]
    [InlineData("ER S01", "ER", 1, null)]
    [InlineData("NYPD Blue S04E05", "NYPD Blue", 4, 5)]
    [InlineData("NYPD Blue S04 E05", "NYPD Blue", 4, 5)]
    [InlineData("NYPD Blue 4x05", "NYPD Blue", 4, 5)]
    [InlineData("NYPD Blue Season 4", "NYPD Blue", 4, null)]
    [InlineData("NYPD Blue S04 1080p", "NYPD Blue", 4, null)]
    [InlineData("Station 19 S03", "Station 19", 3, null)]
    [InlineData("1923 S01E02", "1923", 1, 2)]
    public void ExtractSeasonEpisode_Marker_SplitsTitleAndNumbers(string input, string expectedQuery, int expectedSeason, int? expectedEpisode)
    {
        var (query, season, episode) = Parsing.ExtractSeasonEpisode(input);

        query.Should().Be(expectedQuery);
        season.Should().Be(expectedSeason);
        episode.Should().Be(expectedEpisode);
    }

    [Theory]
    [InlineData("NYPD Blue")]
    [InlineData("S.W.A.T.")]
    [InlineData("S1m0ne")]
    [InlineData("Se7en")]
    [InlineData("S04")]
    [InlineData("")]
    [InlineData("   ")]
    public void ExtractSeasonEpisode_NoUsableMarker_ReturnsInputUnchanged(string input)
    {
        var (query, season, episode) = Parsing.ExtractSeasonEpisode(input);

        query.Should().Be(input);
        season.Should().BeNull();
        episode.Should().BeNull();
    }

    [Fact]
    public void ExtractSeasonEpisode_Null_ReturnsNull()
    {
        var (query, season, episode) = Parsing.ExtractSeasonEpisode(null);

        query.Should().BeNull();
        season.Should().BeNull();
        episode.Should().BeNull();
    }
}

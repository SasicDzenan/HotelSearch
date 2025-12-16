using Moq;
using Xunit;
using HotelSearch.Application.Services;
using HotelSearch.Application.Interfaces;
using HotelSearch.Domain.Models;
using HotelSearch.DTOs.Requests;

public class SearchServiceTests
{
    [Fact]
    public async Task SearchAsync_ShouldSortByPriceThenDistance()
    {
        var hotels = new[]
        {
            new Hotel(Guid.NewGuid(), "Hotel A", 100, new GeoLocation(45.80, 15.90)),
            new Hotel(Guid.NewGuid(), "Hotel B", 80,  new GeoLocation(45.81, 15.91)),
            new Hotel(Guid.NewGuid(), "Hotel C", 80,  new GeoLocation(46.50, 16.00))
        };

        var repo = new Mock<IHotelRepository>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(hotels);

        var ranking = new HotelRankingService();
        var service = new SearchService(repo.Object, ranking);

        var request = new SearchHotelsRequest
        {
            Latitude = 45.81,
            Longitude = 15.98
        };

        var result = await service.SearchAsync(request);

        var list = result.Data!.ToList();

        Assert.Equal(3, list.Count);
        Assert.Equal("Hotel B", list[0].Name);
        Assert.Equal("Hotel C", list[1].Name);
        Assert.Equal("Hotel A", list[2].Name);
    }

    [Fact]
    public async Task SearchAsync_ShouldApplyPaging()
    {
        var hotels = new[]
        {
        new Hotel(Guid.NewGuid(), "Hotel A", 100, new GeoLocation(45.80, 15.90)),
        new Hotel(Guid.NewGuid(), "Hotel B", 80,  new GeoLocation(45.81, 15.91)),
        new Hotel(Guid.NewGuid(), "Hotel C", 70,  new GeoLocation(45.82, 15.92))
    };

        var repo = new Mock<IHotelRepository>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(hotels);

        var ranking = new HotelRankingService();
        var service = new SearchService(repo.Object, ranking);

        var request = new SearchHotelsRequest
        {
            Latitude = 45.81,
            Longitude = 15.98,
            Page = 1,
            PageSize = 2
        };

        var result = await service.SearchAsync(request);
        var list = result.Data!.ToList();

        Assert.Equal(2, list.Count);
        Assert.Equal("Hotel C", list[0].Name);
        Assert.Equal("Hotel B", list[1].Name);
    }

}

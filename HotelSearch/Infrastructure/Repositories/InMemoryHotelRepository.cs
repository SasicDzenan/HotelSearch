using System.Collections.Concurrent;
using HotelSearch.Application.Interfaces;
using HotelSearch.Domain.Models;

namespace HotelSearch.Infrastructure.Repositories;

public class InMemoryHotelRepository : IHotelRepository
{
    private readonly ConcurrentDictionary<Guid, Hotel> _hotels = new();

    public Task<IEnumerable<Hotel>> GetAllAsync()
        => Task.FromResult(_hotels.Values.AsEnumerable());

    public Task<Hotel?> GetByIdAsync(Guid id)
        => Task.FromResult(_hotels.GetValueOrDefault(id));

    public Task AddAsync(Hotel hotel)
    {
        _hotels[hotel.Id] = hotel;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Hotel hotel)
    {
        _hotels[hotel.Id] = hotel;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _hotels.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}

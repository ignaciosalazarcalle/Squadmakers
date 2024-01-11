namespace Squadmakers.Application.Interfaces
{
    public interface IJokeService
    {
        Task<string?> GetJokeAsync(string? name = null);
    }
}

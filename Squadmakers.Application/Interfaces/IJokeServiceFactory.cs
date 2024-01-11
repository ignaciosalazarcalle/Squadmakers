using Squadmakers.Domain.Dtos;

namespace Squadmakers.Application.Interfaces
{
    public interface IJokeServiceFactory
    {
        IJokeService GetInstance(SquadmakersEnums.ServiceType serviceType);
    }
}

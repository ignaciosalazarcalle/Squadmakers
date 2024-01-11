using Squadmakers.Application.Interfaces;
using Squadmakers.Domain.Dtos;

namespace Squadmakers.Application
{
    public class Delegates
    {
        public delegate IJokeService? ServiceResolver(SquadmakersEnums.ServiceType serviceType);
    }
}

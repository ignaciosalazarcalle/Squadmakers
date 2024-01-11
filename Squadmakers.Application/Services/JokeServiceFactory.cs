using Squadmakers.Application.Interfaces;
using Squadmakers.Domain.Dtos;

namespace Squadmakers.Application.Services
{
    public class JokeServiceFactory : IJokeServiceFactory
    {
        private readonly IEnumerable<IJokeService> _jokeServices;

        public JokeServiceFactory(IEnumerable<IJokeService> jokeServices)
        {
            this._jokeServices = jokeServices;
        }

        public IJokeService GetInstance(SquadmakersEnums.ServiceType serviceType)
        {
            return serviceType switch
            {
                SquadmakersEnums.ServiceType.ChuckNorris => GetService(typeof(ChuckNorrisService)),
                SquadmakersEnums.ServiceType.Dad => GetService(typeof(DadService)),
                _ => throw new InvalidOperationException()
            };
        }

        public IJokeService GetService(Type type)
        {
            return _jokeServices.First(x => x.GetType() == type);
        }
    }
}

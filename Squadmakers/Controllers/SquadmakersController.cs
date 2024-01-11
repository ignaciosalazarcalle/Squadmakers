using Microsoft.AspNetCore.Mvc;
using Squadmakers.Application.Interfaces;
using Squadmakers.Domain.Dtos;
using Squadmakers.Infraestructure.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Squadmakers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquadmakersController : ControllerBase
    {
        private readonly IJokeServiceFactory _jokeServiceFactory;
        private readonly IRepositoryJoke<Joke> _jokeRepository;

        public SquadmakersController(
            IJokeServiceFactory jokeServiceFactory,
            IRepositoryJoke<Joke> jokeRepository)
        {
            _jokeServiceFactory = jokeServiceFactory;
            _jokeRepository = jokeRepository;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(@"Devolver� un chiste aleatorio si no se pasa ning�n path param.
            ** Si se env�a el path param habr� que comprobar si tiene el valor �Chuck� o el valor �Dad�
            ** Si tiene el valor �Chuck� se conseguir� el chiste de este API https://api.chucknorris.io
            ** Si tiene el valor �Dad� se conseguir� del API https://icanhazdadjoke.com/api
            ** En caso de que el valor no sea ninguno de esos dos se devolver� el error correspondiente.")]
        public async Task<IActionResult> Get([FromQuery] string? name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    var chuckNorrisService = _jokeServiceFactory.GetInstance(SquadmakersEnums.ServiceType.ChuckNorris);
                    string? randomJoke = await chuckNorrisService.GetJokeAsync();
                    return Ok(randomJoke);
                }

                if (string.Equals(name, "Chuck", StringComparison.OrdinalIgnoreCase))
                {
                    var chuckNorrisService = _jokeServiceFactory.GetInstance(SquadmakersEnums.ServiceType.ChuckNorris);
                    string? randomJoke = await chuckNorrisService.GetJokeAsync(name);
                    return Ok(randomJoke);
                }

                if (string.Equals(name, "Dad", StringComparison.OrdinalIgnoreCase))
                {
                    var dadService = _jokeServiceFactory.GetInstance(SquadmakersEnums.ServiceType.Dad);
                    string? randomJoke = await dadService.GetJokeAsync(name);
                    return Ok(randomJoke);
                }

                return BadRequest($"No se encontr� un chiste para el nombre '{name}'.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error durante la solicitud: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoAccion))]
        [SwaggerOperation("Devolver� el chiste indicado en el par�metro �id�.")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var jokeBase = await _jokeRepository.GetById(id);
                if (jokeBase == null)
                {
                    return BadRequest(new ResultadoAccion
                    {
                        Respuesta = $"No se encontr� un chiste con el identificador '{id}'."
                    });
                }

                return Ok(new ResultadoAccion
                {
                    Respuesta = jokeBase.Descripcion
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error durante la solicitud: {ex.Message}");
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoAccion<Guid>))]
        [SwaggerOperation("Guardar� en una base de datos el chiste (texto pasado por par�metro).")]
        public async Task<IActionResult> Post([FromBody] string joke)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(joke))
                {
                    return BadRequest(new ResultadoAccion
                    {
                        Respuesta = "No se puede crear un chiste vac�o."
                    });
                }

                var jokeBase = new Joke
                {
                    Id = Guid.NewGuid(),
                    Descripcion = joke
                };
                await _jokeRepository.Insert(jokeBase);
                await _jokeRepository.Save();
                return Ok(new ResultadoAccion<Guid>
                {
                    Entidad = jokeBase.Id,
                    Respuesta = "Proceso realizado con �xito."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultadoAccion
                {
                    Respuesta = "Error durante la solicitud: {ex.Message}"
                });
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoAccion))]
        [SwaggerOperation("Actualiza el chiste con el nuevo texto sustituyendo al chiste indicado en el par�metro �id�.")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] string joke)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(joke))
                {
                    return BadRequest(new ResultadoAccion
                    {
                        Respuesta = "No se puede crear un chiste vac�o."
                    });
                }

                var jokeBase = await _jokeRepository.GetById(id);
                if (jokeBase == null)
                {
                    return BadRequest(new ResultadoAccion
                    {
                        Respuesta = $"No se encontr� un chiste con el identificador '{id}'."
                    });
                }

                jokeBase.Descripcion = joke;
                _jokeRepository.Update(jokeBase);
                await _jokeRepository.Save();
                return Ok(new ResultadoAccion
                {
                    Respuesta = "Proceso realizado con �xito."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultadoAccion
                {
                    Respuesta = "Error durante la solicitud: {ex.Message}"
                });
            }
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoAccion))]
        [SwaggerOperation("Elimina el chiste indicado en el par�metro number.")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (await _jokeRepository.Delete(id))
                {
                    await _jokeRepository.Save();
                    return Ok(new ResultadoAccion
                    {
                        Respuesta = "Proceso realizado con �xito."
                    });
                }

                return BadRequest(new ResultadoAccion
                {
                    Respuesta = $"No se encontr� un chiste con el identificador '{id}'."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultadoAccion
                {
                    Respuesta = "Error durante la solicitud: {ex.Message}"
                });
            }
        }
    }
}

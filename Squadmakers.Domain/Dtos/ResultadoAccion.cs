namespace Squadmakers.Domain.Dtos
{
    public class ResultadoAccion
    {
        public string Respuesta { get; set; }
    }

    public class ResultadoAccion<T> : ResultadoAccion
    {
        public T Entidad { get; set; }
    }
}

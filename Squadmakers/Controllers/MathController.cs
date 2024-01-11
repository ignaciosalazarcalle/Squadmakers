using Microsoft.AspNetCore.Mvc;

namespace Squadmakers.Controllers
{
    public class MathController : ControllerBase
    {
        [HttpGet("MinimoComunMultiplo")]
        public IActionResult Get([FromQuery] int[] numbers)
        {
            try
            {
                if (numbers.Length == 0)
                {
                    return BadRequest("Se requiere al menos un número para calcular el Mínimo Común Múltiplo.");
                }

                // Calcular el mínimo común múltiplo (LCM)
                int mcm = CalculateLeastCommonMultiple(numbers);

                return Ok(mcm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error durante la operación: {ex.Message}");
            }
        }

        [HttpGet("Increment")]
        public IActionResult IncrementNumber([FromQuery] int number)
        {
            try
            {
                int result = number + 1;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error durante la operación: {ex.Message}");
            }
        }

        #region Private Methods

        private int CalculateLeastCommonMultiple(int[] numbers)
        {
            if (numbers.Length == 1)
            {
                return numbers[0];
            }

            int lcm = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                lcm = CalculateMinimoComunMultiplo(lcm, numbers[i]);
            }

            return lcm;
        }

        private int CalculateMinimoComunMultiplo(int a, int b)
        {
            return Math.Abs(a * b) / CalculateMaximoComunDivisor(a, b);
        }

        private int CalculateMaximoComunDivisor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        #endregion
    }
}

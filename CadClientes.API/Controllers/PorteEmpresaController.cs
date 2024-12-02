using CadClientes.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadClientes.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PorteEmpresaController : ControllerBase
    {
        private readonly IPorteEmpresaRepository _porteEmpresaRepository;

        public PorteEmpresaController(IPorteEmpresaRepository porteEmpresaRepository)
        {
            this._porteEmpresaRepository = porteEmpresaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var porteEmpresas = await _porteEmpresaRepository.GetAll();
            return Ok(porteEmpresas);
        }

        //[HttpGet("test-connection")] 
        //public async Task<IActionResult> TestConnection() { 
        //    try { 
        //        var porteEmpresas = await _porteEmpresaRepository.GetAll();
        //        //if (porteEmpresas.Count == 0) { 
        //        //    return Ok("Nenhum registro encontrado."); 
        //        //} 
        //        return Ok(porteEmpresas); 
        //    } catch (Exception ex) { 
        //        return StatusCode(500, $"Erro ao conectar ao MongoDB: {ex.Message}"); 
        //    } 
        //}
    }
}

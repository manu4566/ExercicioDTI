using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.DTO;
using Modelo.Application.Interfaces;

namespace Modelo.Host.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IProcessarMsgAcaoUsuarioAppService _processarMensagemAppService;
        
        public UsuarioController(IProcessarMsgAcaoUsuarioAppService processarMensagemAppService)                                  
        {
            _processarMensagemAppService = processarMensagemAppService;
            
        }

        [HttpPost]
       
        [ProducesResponseType(typeof(MensagemRetornoAcaoUsuario), StatusCodes.Status200OK)]       
        public async Task<ActionResult<MensagemRetornoAcaoUsuario>> ProcessarMensagem([FromBody] MensagemAcaoUsuario mensagem)
        {            
            return await _processarMensagemAppService.ProcessarMsgAcaoUsuario(mensagem);
        }
    }
}

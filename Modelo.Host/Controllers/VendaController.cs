using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.DTO;
using Modelo.Application.Interfaces;

namespace Modelo.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IProcessarMsgAcaoVendaAppService _processarMensagemAppService;

        public VendaController(IProcessarMsgAcaoVendaAppService processarMensagemAppService)
        {
            _processarMensagemAppService = processarMensagemAppService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(MensagemRetornoAcaoVenda), StatusCodes.Status200OK)]
        public async Task<ActionResult<MensagemRetornoAcaoVenda>> ProcessarMensagem([FromBody] MensagemAcaoVenda mensagem)
        {
            return await _processarMensagemAppService.ProcessarMsgAcaoVenda(mensagem);
        }
    }
}

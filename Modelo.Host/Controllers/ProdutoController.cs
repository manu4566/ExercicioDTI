using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.DTO;
using Modelo.Application.Interfaces;

namespace Modelo.Host.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProcessarMsgAcaoProdutoAppService _processarMensagemAppService;

        public ProdutoController(IProcessarMsgAcaoProdutoAppService processarMensagemAppService)
        {
            _processarMensagemAppService = processarMensagemAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MensagemRetornoAcaoProduto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MensagemRetornoAcaoProduto>> ProcessarMensagem([FromBody] MensagemAcaoProduto mensagem)
        {
            return await _processarMensagemAppService.ProcessarMsgAcaoProduto(mensagem);
        }
    }
}

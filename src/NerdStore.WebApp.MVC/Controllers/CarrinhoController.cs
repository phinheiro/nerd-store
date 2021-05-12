using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediatr;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatrHandler _mediatrHandler;
        public CarrinhoController(INotificationHandler<DomainNotification> notification,
                                  IProdutoAppService produtoAppService,
                                  IMediatrHandler mediatrHandler,
                                  IPedidoQueries pedidoQueries) : base(notification, mediatrHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatrHandler = mediatrHandler;
            _pedidoQueries = pedidoQueries;
        }

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatrHandler.EnviarComando(command);

            if(OperacaoValida())
                return RedirectToAction("Index");

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        // [HttpPost("remover-item")]
        // public async Task<IActionResult> RemoverItem(Guid id){
        //     var produto = await _produtoAppService.ObterPorId(id);
        //     if (produto == null) return BadRequest();

        //     var command = new RemoverItemPedidoCommand(ClienteId, id);
        //     await _mediatrHandler.EnviarComando(command);

        //     if(OperacaoValida())
        //         return RedirectToAction("Index");

        //     return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        // }

        // [HttpPost("atualizar-item")]
        // public async Task<IActionResult> AtualizarItem(Guid id, int quantidade){
        //     var produto = await _produtoAppService.ObterPorId(id);
        //     if (produto == null) return BadRequest();

        //     var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
        //     await _mediatrHandler.EnviarComando(command);

        //     if(OperacaoValida())
        //         return RedirectToAction("Index");

        //     return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        // }

        // [HttpPost("aplicar-voucher")]
        // public async Task<IActionResult> AplicarVoucher(string voucherCodigo){
        //     var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
        //     await _mediatrHandler.EnviarComando(command);

        //     if (OperacaoValida())
        //         return RedirectToAction("Index");

        //     return RedirectToAction("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        // }
    }
}
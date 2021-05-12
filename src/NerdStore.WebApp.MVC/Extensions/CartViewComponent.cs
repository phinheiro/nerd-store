using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPedidoQueries _pedidoQueries;
        public CartViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        // TODO: Obter cliente logado
        protected Guid ClienteId = Guid.Parse("544c109d-6096-457f-af08-adfd8e523f83");

        public async Task<IViewComponentResult> InvokeAsync(){
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Itens.Count ?? 0;

            return View(itens);
        }
    }
}
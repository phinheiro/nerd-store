using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext _context;
        public PedidoRepository(VendasContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Pedido> ObterPorId(Guid id) =>
            await _context.Pedidos.FindAsync(id);

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId) =>
            await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();

        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
            if (pedido == null) return null;

            await _context.Entry(pedido).Collection(i => i.PedidoItens).LoadAsync();

            if (pedido.VoucherId != null)
            {
                await _context.Entry(pedido).Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Adicionar(Pedido pedido) =>
            _context.Pedidos.Add(pedido);

        public void Atualizar(Pedido pedido) =>
            _context.Pedidos.Update(pedido);


        public async Task<PedidoItem> ObterItemPorId(Guid id) =>
            await _context.PedidoItens.FindAsync(id);

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId) =>
            await _context.PedidoItens.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);

        public void AdicionarItem(PedidoItem pedidoItem) =>
            _context.PedidoItens.Add(pedidoItem);

        public void AtualizarItem(PedidoItem pedidoItem) =>
            _context.PedidoItens.Update(pedidoItem);

        public void RemoverItem(PedidoItem pedidoItem) =>
            _context.PedidoItens.Remove(pedidoItem);

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo) =>
            await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);

        public void Dispose() => _context?.Dispose();
    }
}
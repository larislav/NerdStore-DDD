﻿using MediatR;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Infrastructure;
using NerdStore.Catalogo.Infrastructure.Repository;
using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Infrastructure;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catálogo
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<VendasDbContext>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();

            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();

        }
    }
}

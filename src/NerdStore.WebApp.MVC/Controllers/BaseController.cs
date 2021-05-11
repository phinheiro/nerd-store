using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediatr;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatrHandler _mediatorHandler;
        protected Guid ClienteId = Guid.Parse("544c109d-6096-457f-af08-adfd8e523f83");

        protected BaseController(INotificationHandler<DomainNotification> notification, IMediatrHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notification;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida() => !_notifications.TemNotificacao();

        protected IEnumerable<string> ObterMensagensErro() =>
            _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        protected void NotificarErro(string codigo, string mensagem) =>
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notification;
        public SummaryViewComponent(INotificationHandler<DomainNotification> notification)
        {
            _notification = (DomainNotificationHandler)notification;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var notificacoes = await Task.FromResult(_notification.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return View();
        }
    }
}
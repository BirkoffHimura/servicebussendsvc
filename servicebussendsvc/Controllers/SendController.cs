using System.Diagnostics;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace servicebussendsvc.Controllers
{
    [ApiController]
    [Route("/Send")]
    public class SendController : Controller
    {
        private readonly ServiceBus _sb;
        public SendController(IOptions<ServiceBus> sb)
        {
            _sb = sb.Value;
        }
        [HttpPost]
        public async Task Send(string body)
        {
            var retStr = string.Empty;
            ServiceBusClient client;
            ServiceBusSender sender;
            if (string.IsNullOrEmpty(this._sb.IdentityClientId)) 
            {
                client = new ServiceBusClient(this._sb.FullyQualifiedNamespace, new DefaultAzureCredential());
            } else
            {
                client = new ServiceBusClient(this._sb.FullyQualifiedNamespace, new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true, ManagedIdentityClientId = this._sb.IdentityClientId }));
            }
            sender = client.CreateSender(this._sb.Topic);
            ServiceBusMessage message = new ServiceBusMessage(body);
            await sender.SendMessageAsync(message);
        }
    }
}

using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;
using System.Diagnostics;

namespace NETCoreMVCwithMSGraph.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            //Used this link as a reference for authentication: https://learn.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=CS
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Found ClientSecretCredential: https://learn.microsoft.com/en-us/dotnet/api/azure.identity.clientsecretcredential?view=azure-dotnet
            ClientSecretCredential credential = new ClientSecretCredential("tenantId", "clientId", "clientSecret");

            GraphServiceClient graphClient = new GraphServiceClient(credential, scopes, "https://graph.microsoft.com/v1.0");
            var me = await graphClient.Me.GetAsync();


            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
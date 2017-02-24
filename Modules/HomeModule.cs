using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace SalonApp
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["index.cshtml", AllStylists];
            };
            Get["/clients"] = _ => {
                List<Client> AllClients = Client.GetAll();
                return View["clients.csthml, AllClients"];
            };
            Get["/stylists"] = _ => {
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["stylists.cshtml", AllStylists];
            };
            Get["/stylists/new"] = _ => {
                return View["stylist-form.cshtml"];
            };
            Post["/stylists/new"] = _ => {
                Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-phone"]);
                newStylist.Save();
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["stylists.cshtml", AllStylists];
            };
            Get["/clients/new"] = _ => {
                List<Client> AllClients = Client.GetAll();
                return View["clients_form.cshtml", AllClients];
            };
            Post["/clients/new"] = _ => {
                Client newClient = new Client(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["stylist-name"]);
                newClient.Save();
                return View["clients.cshtml"];
            };
        }
    }
}

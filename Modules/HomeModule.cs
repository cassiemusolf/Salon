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
                return View["clients.cshtml", AllClients];
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
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["client-form.cshtml", AllStylists];
            };
            Post["/clients/new"] = _ => {
                Client newClient = new Client(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["stylist-id"]);
                newClient.Save();
                List<Client> AllClients = Client.GetAll();
                return View["clients.cshtml", AllClients];
            };
            Post["/clients/delete"] = _ => {
                Client.DeleteAll();
                List<Client> AllClients = Client.GetAll();
                return View["clients.cshtml", AllClients];
            };
            Post["/stylists/delete"] = _ => {
                Stylist.DeleteAll();
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["stylists.cshtml", AllStylists];
            };
            Get["/stylist/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                var SelectedStylist = Stylist.Find(parameters.id);
                var StylistClients = SelectedStylist.GetClients();
                model.Add("stylist", SelectedStylist);
                model.Add("clients", StylistClients);
                return View["stylist.cshtml", model];
            };
            Get["stylist/edit/{id}"] = parameters => {
                Stylist SelectedStylist = Stylist.Find(parameters.id);
                return View["stylist_edit.cshtml", SelectedStylist];
            };
            Patch["/stylist/edit/{id}"] = parameters => {
                Stylist SelectedStylist = Stylist.Find(parameters.id);
                SelectedStylist.Update(Request.Form["stylist-name"]);
                return View["success.cshtml"];
            };
            Get["stylist/delete/{id}"] = parameters => {
                Stylist SelectedStylist = Stylist.Find(parameters.id);
                return View["stylist_delete.cshtml", SelectedStylist];
            };
            Delete["stylist/delete/{id}"] = parameters => {
                Stylist SelectedStylist = Stylist.Find(parameters.id);
                SelectedStylist.Delete();
                List<Stylist> AllStylists = Stylist.GetAll();
                return View["stylists.cshtml", AllStylists];
            };
            Get["/clients/{id}"] = parameters => {
                Client client = Client.Find(parameters.id);
                return View["client.cshtml", client];
            };
            Get["/client/edit/{id}"] = parameters => {
                Client SelectedClient = Client.Find(parameters.id);
                return View["client_edit.cshtml", SelectedClient];
            };
            Patch["/client/edit/{id}"] = parameters => {
                Client SelectedClient = Client.Find(parameters.id);
                SelectedClient.Update(Request.Form["client-name"], Request.Form["client-phone"]);
                return View["success.cshtml"];
            };
            Get["client/delete/{id}"] = parameters => {
                Client SelectedClient = Client.Find(parameters.id);
                return View["client_delete.cshtml", SelectedClient];
            };
            Delete["client/delete/{id}"] = parameters => {
                Client SelectedClient = Client.Find(parameters.id);
                SelectedClient.Delete();
                List<Client> AllClients = Client.GetAll();
                return View["clients.cshtml", AllClients];
            };
        }
    }
}

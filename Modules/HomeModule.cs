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
  }
}
}

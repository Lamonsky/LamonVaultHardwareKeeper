using WebInterface.Helpers;
using Data;
using Microsoft.AspNetCore.Mvc;
using Data.Computers.SelectVMs;
using WebInterface.Models;

namespace WebInterface.Views.Shared.Components
{
    [ViewComponent(Name = "PageContentViewComponent")]
    public class PageContentViewComponentClass : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            List<PageContentVM> orders = await RequestHelper.SendRequestAsync<object, List<PageContentVM>>(URLs.PAGECONTENT, HttpMethod.Get, null, GlobalData.AccessToken);

            PageContentVM vm = orders.FirstOrDefault(p => p.Title == type);
            if (vm == null)
            {
                return View("Default", "");
            }
            else
            {
                return View("Default", vm.Content);
            }            
        }
    }
}

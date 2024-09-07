using WebInterface.Helpers;
using Data;
using Microsoft.AspNetCore.Mvc;
using Data.Computers.SelectVMs;

namespace WebInterface.Views.Shared.Components
{
    [ViewComponent(Name = "PageContentViewComponent")]
    public class PageContentViewComponentClass : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            List<PageContentVM> orders = await RequestHelper.SendRequestAsync<object, List<PageContentVM>>(URLs.PAGECONTENT, HttpMethod.Get, null, null);

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

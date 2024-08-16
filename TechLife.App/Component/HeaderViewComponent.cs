using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Models;
using TechLife.Common.Extension;
using TechLife.Common;
using TechLife.Service;

namespace TechLife.App.Component
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly INgonNguService _ngonNguService;

        public HeaderViewComponent(INgonNguService ngonNguService)
        {
            _ngonNguService = ngonNguService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = Request.GetUser();

            var languages = await _ngonNguService.GetAll();
            var currentLanguageId = HttpContext
                .Session
                .GetString(Common.SystemConstants.AppSettings.DefaultLanguageId);
            var items = languages.Select(x => new SelectListItem()
            {
                Text = x.Ten,
                Value = x.Id.ToString(),
                Selected = currentLanguageId == null ? x.IsDefault : currentLanguageId == x.Id.ToString()
            });
            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = currentLanguageId,
                Languages = items.ToList(),
                UserInfo = model,
                ReturnUrl = Request.GetRawUrl()
            };

            return await Task.Run(() =>
                 View("_Header", navigationVm)
            );
        }
    }
}
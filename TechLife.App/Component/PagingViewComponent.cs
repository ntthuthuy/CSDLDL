﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Models;
using TechLife.Common;

namespace TechLife.App.Component
{
    public class PagingViewComponent : ViewComponent
    {
        async Task OptionPage(int total = 0)
        {
            await Task.Run(() =>
            {
                var list = new List<SelectListItem>();
                if (total >= SystemConstants.pageSize)
                {
                    list.Add(new SelectListItem() { Text = "" + SystemConstants.pageSize, Value = SystemConstants.pageSize.ToString() });
                    list.Add(new SelectListItem() { Text = "30", Value = "30" });
                    list.Add(new SelectListItem() { Text = "60", Value = "60" });
                    list.Add(new SelectListItem() { Text = "90", Value = "90" });
                    list.Add(new SelectListItem() { Text = "120", Value = "120" });
                }

                ViewBag.listPage = list;
            });
        }
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase page)
        {
            var model = new PagingViewModel();

            model.PageIndex = page.PageIndex;
            model.Total = page.TotalRecords;
            model.PageSize = page.PageSize;

            await OptionPage(page.TotalRecords);

            return View("_Paging", model);
        }
    }
}

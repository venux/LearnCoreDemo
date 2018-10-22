using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myWebApp.Pages
{
    public class ModelTestModel : PageModel
    {
        public string Message { get; private set; } = "附带Model的Razor Page测试";

        public void OnGet()
        {
            Message += $"当前服务器时间：{DateTime.Now}";
        }
    }
}
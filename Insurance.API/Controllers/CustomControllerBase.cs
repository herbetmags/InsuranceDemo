using Insurance.Common.Models.AppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Insurance.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected readonly PageSettings PageSettings;
        public CustomControllerBase(IOptions<PageSettings> pageSettings)
        {
            PageSettings = pageSettings.Value;
            //TODO Logging
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Restful.Api.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restful.Api.Controllers.V2
{
    [ApiVersion("2")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        [Produces(MediaTypeNames.Application.Xml)]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return new JsonResult(companies);
        }
    }
}

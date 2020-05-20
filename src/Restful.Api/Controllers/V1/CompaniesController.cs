using Microsoft.AspNetCore.Mvc;
using Restful.Api.Entities;
using Restful.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restful.Api.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
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
        //[Produces(MediaTypeNames.Application.Xml)]
        [HttpGet]
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            //return new JsonResult(companies);
            return companies;
        }
    }
}

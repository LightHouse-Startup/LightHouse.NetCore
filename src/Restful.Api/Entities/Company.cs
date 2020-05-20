using System;
using System.Collections.Generic;

namespace Restful.Api.Entities
{
    public class Company
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公司介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 公司员工
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}

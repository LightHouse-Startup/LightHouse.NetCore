using System;

namespace Restful.Api.Entities
{
    public class Employee
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 员工所属公司Id
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public Company Company { get; set; }
    }
}

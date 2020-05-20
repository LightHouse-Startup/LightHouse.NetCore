using System;

namespace FootballManager
{
    public class TransferApplication
    {
        public int Id { get; set; }

        /// <summary>
        /// 球员名字
        /// </summary>
        /// <value></value>
        public string PlayerName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        /// <value></value>
        public int PlayerAge { get; set; }

        /// <summary>
        /// 转让费（百万）
        /// </summary>
        /// <value></value>
        public decimal TransferFee { get; set; }

        /// <summary>
        /// 年薪（百万）
        /// </summary>
        /// <value></value>
        public decimal AnnualSalary { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        /// <value></value>
        public int ContractYears { get; set; }

        /// <summary>
        /// 是否超级巨星
        /// </summary>
        /// <value></value>
        public bool IsSuperStar { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        /// <value></value>
        public int PlayerStrength { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        /// <value></value>
        public int PlayerSpeed { get; set; }
    }
}

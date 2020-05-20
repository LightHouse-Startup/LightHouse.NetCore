using System.Collections.Generic;

namespace Startup.Webapi.Dtos
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        /// <value></value>
        public ICollection<Material> Materials { get; set; }
    }

    /// <summary>
    /// 原材料
    /// </summary>
    public class Material
    {
        /// <summary>
        /// 原材料Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 原材料名称
        /// </summary>
        public string Name { get; set; }
    }
}

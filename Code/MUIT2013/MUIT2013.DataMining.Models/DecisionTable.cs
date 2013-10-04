using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.Models
{
    public class DecisionTable
    {
        /// <summary>
        /// Tập các thuộc tính quyết định
        /// </summary>
        public int[] D { get; set; }

        /// <summary>
        /// Tập các thuộc tính điều kiện
        /// </summary>
        public int[] A { get; set; }

        /// <summary>
        /// Tập các giá trị với index cấp 1 là index của 1 thuộc tính trong tập thuộc tính.
        /// Ví dụ: V[1] = [1.4, 2.3,5.10]. V[1] là tập giá trị của thuộc tính thứ 2 trong tập thuộc tính.        
        /// </summary>
        public double[][] V { get; set; }
        
        /// <summary>
        ///Tập các đối tượng
        /// </summary>
        public double[][] U { get; set; }
    }
}

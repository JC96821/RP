using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EASYS.Models
{
    public class classManager
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        public string class_cd { get; set; }

        /// <summary>
        /// 班级名
        /// </summary>
        public string class_nm { get; set; }

        /// <summary>
        /// 专业编号
        /// </summary>
        public string profession_cd { get; set; }

        /// <summary>
        /// 学院编号
        /// </summary>
        public string college_cd { get; set; }

        /// <summary>
        /// 用户编号 班级导师
        /// </summary>
        public string user_cd { get; set; }

        /// <summary>
        /// 处理flg 0:登录 1:更新
        /// </summary>
        public string exec_flg { get; set; }
    }
}

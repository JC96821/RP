using System;

namespace EASYS.Models
{
    public class userManager
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        public string user_cd { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_nm { get; set; }

        /// <summary>
        /// 用户区分 0:学生 1:老师 9:管理员
        /// </summary>
        public string distinction { get; set; }

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
        /// 专业名
        /// </summary>
        public string profession_nm { get; set; }

        /// <summary>
        /// 学院名
        /// </summary>
        public string college_nm { get; set; }

        /// <summary>
        /// 删除标记 0:在用 1:已删除
        /// </summary>
        public string del_flg { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public string upd_dt { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string upd_user { get; set; }


    }
}

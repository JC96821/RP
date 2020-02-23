namespace EASYS.Models
{
    public class curriculumManager
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string curriculum_cd { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        public string curriculum_nm { get; set; }

        /// <summary>
        /// 教师编号
        /// </summary>
        public string user_cd { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
        public string user_nm { get; set; }

        /// <summary>
        /// 学期
        /// </summary>
        public string term { get; set; }

        /// <summary>
        /// 上课时间: 周一
        /// </summary>
        public string monday { get; set; }
        
        /// <summary>
        /// 上课时间: 周二
        /// </summary>
        public string tuesday { get; set; }

        /// <summary>
        /// 上课时间: 周三
        /// </summary>
        public string wednesday { get; set; }

        /// <summary>
        /// 上课时间: 周四
        /// </summary>
        public string thursday { get; set; }

        /// <summary>
        /// 上课时间: 周五
        /// </summary>
        public string friday { get; set; }

        /// <summary>
        /// 上课时间: 周六
        /// </summary>
        public string saturday { get; set; }

        /// <summary>
        /// 上课时间: 周日
        /// </summary>
        public string sunday { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public string login_dt { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public string login_user { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string upd_dt { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        public string upd_user { get; set; }

        /// <summary>
        /// 处理flg 0:登录 1:更新
        /// </summary>
        public string exec_flg { get; set; }

    }
}

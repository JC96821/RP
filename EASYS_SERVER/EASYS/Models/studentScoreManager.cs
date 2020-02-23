namespace EASYS.Models
{
    public class studentScoreManager
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
        /// 学期
        /// </summary>
        public string term { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string user_cd { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_nm { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public string score { get; set; }

    }
}

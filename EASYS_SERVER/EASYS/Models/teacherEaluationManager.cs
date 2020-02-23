namespace EASYS.Models
{
    public class teacherEaluationManager
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
        /// 教师编号
        /// </summary>
        public string teacher_cd { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public string student_cd { get; set; }

        /// <summary>
        /// 对教师评价
        /// </summary>
        public string evaluation_teacher { get; set; }
    }
}

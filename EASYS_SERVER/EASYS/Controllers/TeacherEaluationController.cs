using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherEaluationController : ControllerBase
    {

        private enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1     //失败
        }

        /// <summary>
        /// 获取教师下拉数据
        /// </summary>
        /// <returns></returns>
        public string getTeacherList(teacherEaluationManager item)
        {
            string sql = $@"
                            SELECT
                                DISTINCT
                                c.user_cd,
                                u.user_nm
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_curriculum c
                                    ON c.curriculum_cd = uc.curriculum_cd
                                LEFT JOIN m_user u 
                                    ON c.user_cd = u.user_cd
                            WHERE
                                uc.user_cd = '{item.student_cd}'
                        ";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public string search(teacherEaluationManager item)
        {
            string sql = $@"
                            SELECT
                                c.curriculum_cd,
                                c.curriculum_nm,
                                c.term,
                                u.user_nm,
                                uc.evaluation_teacher
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_curriculum c
                                    ON c.curriculum_cd = uc.curriculum_cd
                                LEFT JOIN m_user u 
                                    ON c.user_cd = u.user_cd
                            WHERE
                                uc.user_cd = '{item.student_cd}'
                        ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND c.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            if (!string.IsNullOrEmpty(item.teacher_cd)) sql += $" AND c.user_cd = '{item.teacher_cd}'";
            sql += " ORDER BY c.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int save(teacherEaluationManager item)
        {
            string sql = $@"
                        UPDATE
                            m_user_curriculum
                        SET
                            evaluation_teacher = '{item.evaluation_teacher}'
                        WHERE
                            user_cd = '{item.student_cd}'
                            AND curriculum_cd = '{item.curriculum_cd}'
                        ";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
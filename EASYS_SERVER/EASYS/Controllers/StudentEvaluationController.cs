using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentEvaluationController : ControllerBase
    {

        public enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1    //失败
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string search(studentEvaluationManager item)
        {
            string sql = $@"
                            SELECT
                                DISTINCT
                                c.curriculum_cd,
                                c.curriculum_nm,
                                c.term
                            FROM
                                m_curriculum c
                            WHERE
                                c.user_cd = '{item.user_cd}'
                        ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND c.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            sql += " ORDER BY c.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 获取学生评价数据
        /// </summary>
        /// <returns></returns>
        public string getStudentEvaluation(studentEvaluationManager item)
        {
            string sql = $@"
                            SELECT 
                                uc.user_cd,
                                u.user_nm,
                                uc.evaluation_students
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_curriculum c
                                    ON uc.curriculum_cd = c.curriculum_cd
                                LEFT JOIN m_user u
                                    ON uc.user_cd = u.user_cd
                            WHERE
                                uc.curriculum_cd = '{item.curriculum_cd}'
                            ORDER BY 
                                uc.user_cd
                        ";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(studentEvaluationManager item)
        {
            string sql = $@"
                            UPDATE
                                m_user_curriculum
                            SET
                                evaluation_students = '{item.evaluation_students}'
                            WHERE
                                curriculum_cd = '{item.curriculum_cd}'
                                AND user_cd = '{item.user_cd}'
                        ";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
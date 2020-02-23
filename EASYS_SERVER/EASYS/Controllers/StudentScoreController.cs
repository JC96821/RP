using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentScoreController : ControllerBase
    {

        public enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1     //失败
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string search(studentScoreManager item)
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
        /// 获取对应课程学生信息
        /// </summary>
        /// <returns></returns>
        public string getCurriculumData(studentScoreManager item)
        {
            string sql = $@"
                            SELECT 
                                u.user_cd,
                                u.user_nm,
                                convert(uc.score,char) AS score
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_user u
                                    ON uc.user_cd = u.user_cd
                            WHERE
                                uc.curriculum_cd = '{item.curriculum_cd}' 
                            ORDER BY u.user_cd
                        ";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int save(studentScoreManager item)
        {
            string sql = $@"
                            UPDATE 
                                m_user_curriculum
                            SET
                                score = '{item.score}'
                            WHERE
                                user_cd = '{item.user_cd}'
                                AND curriculum_cd = '{item.curriculum_cd}'
                        ";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.SUCCESS;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
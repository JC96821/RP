using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChooseCoursesAndTeachersController : ControllerBase
    {

        private enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1     //失败
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public string search(ccatManager item)
        {
            string sql = $@"
                            SELECT
                                c.curriculum_cd,
                                c.curriculum_nm,
                                c.term,
                                u.user_nm
                            FROM
                                m_curriculum c
                                LEFT JOIN m_user u
                                    ON c.user_cd = u.user_cd
                            WHERE
                                c.curriculum_cd NOT IN (
                                    SELECT 
                                        curriculum_cd
                                    FROM 
                                        m_user_curriculum
                                    WHERE
                                        user_cd = '{item.user_cd}'
                                )
                        ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND c.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            sql += " ORDER BY c.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int save(ccatManager item)
        {
            string sql = $@"
                        INSERT INTO 
                            m_user_curriculum
                        (
                            user_cd,
                            curriculum_cd,
                            score,
                            evaluation_teacher,
                            evaluation_students
                        )VALUES(
                            '{item.user_cd}',
                            '{item.curriculum_cd}',
                            '0',
                            '0',
                            '0'
                        )
                    ";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
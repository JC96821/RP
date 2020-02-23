using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentMangementController : ControllerBase
    {

        public enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1,    //失败
            NOTEXISTS = -2  //学生在当前课表中不存在
        }

        /// <summary>
        /// 获取学生下拉数据
        /// </summary>
        /// <returns></returns>
        public string getUserList(studentMangementManager item)
        {
            string sql = $@"
                        SELECT 
                            user_cd, 
                            user_nm 
                        FROM 
                            m_user 
                        WHERE 
                            del_flg = '0' 
                            AND distinction = '0' 
                            AND user_cd NOT IN (
                                SELECT 
                                    user_cd 
                                FROM 
                                    m_user_curriculum
                                WHERE
                                    curriculum_cd = '{item.curriculum_cd}'
                            )
                        ";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string search(studentMangementManager item)
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
        public string getCurriculumData(studentMangementManager item)
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
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int delete(studentMangementManager item)
        {
            //删除前进行检查，如果该课程本没有登记过学生，默认删除成功
            string chk_sql = $"SELECT 1 FROM m_user_curriculum WHERE curriculum_cd = '{item.curriculum_cd}' AND user_cd = '{item.user_cd}'";
            DataTable dt = DbCommand.doSearch(chk_sql);
            if(dt ==null || dt.Rows.Count == 0)
            {
                return (int)SAVE_RESULT.NOTEXISTS;
            }
            //删除实行
            string sql = $"DELETE FROM m_user_curriculum WHERE curriculum_cd = '{item.curriculum_cd}' AND user_cd = '{item.user_cd}'";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(studentMangementManager item)
        {
            string sql = $@"
                        INSERT INTO
                            m_user_curriculum
                        (
                            user_cd
                            ,curriculum_cd
                            ,score
                            ,evaluation_teacher
                            ,evaluation_students
                        )VALUES(
                            '{item.user_cd}'
                            ,'{item.curriculum_cd}'
                            ,'0'
                            ,'0'
                            ,'0'
                        );";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
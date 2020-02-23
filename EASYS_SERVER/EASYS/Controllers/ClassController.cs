using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {

        private enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1,    //失败
            EXISTS = -2     //班级编号已存在
        }

        /// <summary>
        /// 获取班级导师列表
        /// </summary>
        /// <returns></returns>
        public string getTeacherList()
        {
            string sql = $"SELECT user_cd, user_nm FROM m_user WHERE distinction = '1' AND del_flg = '0'";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 获取专业列表
        /// </summary>
        /// <returns></returns>
        public string getProfessionList()
        {
            string sql = $"SELECT profession_cd, profession_nm FROM m_profession";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 获取学院列表
        /// </summary>
        /// <returns></returns>
        public string getCollegeList()
        {
            string sql = $"SELECT college_cd, college_nm FROM m_college";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public string search(classManager item)
        {
            string sql = $@"
                            SELECT 
                                c.class_cd
                                ,c.class_nm
                                ,p.profession_cd
                                ,p.profession_nm
                                ,co.college_nm
                                ,c.user_cd
                                ,u.user_nm
                            FROM 
                                m_class c
                                LEFT JOIN m_user u
                                    ON c.user_cd = u.user_cd
                                LEFT JOIN m_profession p
                                    ON c.profession_cd = p.profession_cd
                                LEFT JOIN m_college co
                                    ON p.college_cd = co.college_cd
                            WHERE 1=1 
                        ";
            if (!string.IsNullOrEmpty(item.class_cd)) sql += $" AND c.class_cd LIKE '{item.class_cd}%'";
            if (!string.IsNullOrEmpty(item.class_nm)) sql += $" AND c.class_nm LIKE '%{item.class_nm}%'";
            if (!string.IsNullOrEmpty(item.user_cd)) sql += $" AND c.user_cd = '{item.user_cd}'";
            if (!string.IsNullOrEmpty(item.profession_cd)) sql += $" AND c.profession_cd = '{item.profession_cd}'";
            if (!string.IsNullOrEmpty(item.college_cd)) sql += $" AND p.college_cd = '{item.college_cd}'";
            sql += " ORDER BY c.class_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int save(classManager item)
        {
            string chk_sql = $"SELECT 1 FROM m_class WHERE class_cd = '{item.class_cd}'";
            DataTable dt = DbCommand.doSearch(chk_sql);
            string sql = "";
            //该班级不存在登录
            if(dt == null || dt.Rows.Count == 0)
            {
                sql = $@"
                    INSERT INTO 
                        m_class
                    (
                        class_cd,
                        class_nm,
                        profession_cd,
                        user_cd
                    )VALUES(
                        '{item.class_cd}',
                        '{item.class_nm}',
                        '{item.profession_cd}',
                        '{item.user_cd}'
                    )
                ";
            }
            //该班级已存在，更新
            else
            {
                //如果新是登录的情况，且班级编号有重复，那么返回错误
                if (item.exec_flg == "0")
                {
                    return (int)SAVE_RESULT.EXISTS;
                }
                sql = $@"
                        UPDATE 
                            m_class
                        SET
                            user_cd = '{item.user_cd}'
                        WHERE
                            class_cd = '{item.class_cd}'
                    ";
            }
            //执行失败
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            //成功
            return (int)SAVE_RESULT.SUCCESS;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int delete(classManager item)
        {
            string sql = $"DELETE FROM m_class WHERE class_cd = '{item.class_cd}'";
            //执行失败
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            //成功
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
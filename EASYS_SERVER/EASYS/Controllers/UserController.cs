using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1,    //失败
            NOTESISTS = -2, //用户不存在
            EXISTS = -3     //该班级已登记过该用户
        }

        /// <summary>
        /// 获取班级列表
        /// </summary>
        /// <returns></returns>
        public string getClassList()
        {
            string sql = $"SELECT DISTINCT class_cd, class_nm FROM m_class";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string search(userManager item)
        {
            string sql = $@"
                        SELECT 
                            u.user_cd,
                            u.user_nm,
                            u.tell_phone,
                            u.email,
                            c.class_cd,
                            c.class_nm,
                            CASE
                                WHEN u.distinction = '0' THEN '学生' 
                                WHEN u.distinction = '1' THEN '教师'
                                WHEN u.distinction = '9' THEN '管理员'
                                ELSE '其他未在校人员'
                            END AS distinction, 
                            CASE
                                WHEN u.del_flg = '0' THEN '在校'
                                ELSE '退校'
                            END AS del_flg
                        FROM m_user u 
                        LEFT JOIN m_user_class uc
                            ON u.user_cd = uc.user_cd
                        LEFT JOIN m_class c
                            ON uc.class_cd = c.class_cd
                        WHERE 1 = 1";
            if (!string.IsNullOrEmpty(item.user_cd)) sql += $" AND u.user_cd LIKE '{item.user_cd}%'";
            if (!string.IsNullOrEmpty(item.user_nm)) sql += $" AND u.user_nm LIKE '%{item.user_nm}%'";
            if (!string.IsNullOrEmpty(item.distinction)) sql += $" AND u.distinction = '{item.distinction}'";
            if (!string.IsNullOrEmpty(item.del_flg)) sql += $" AND u.del_flg = '{item.del_flg}'";
            sql += "    ORDER BY u.user_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(userManager item)
        {
            string chk_sql = $"SELECT 1 FROM m_user WHERE user_cd = '{item.user_cd}' AND del_flg = '0'";
            DataTable chk_dt = DbCommand.doSearch(chk_sql);
            //该用户不存在
            if(chk_dt == null || chk_dt.Rows.Count == 0)
            {
                return (int)SAVE_RESULT.NOTESISTS;
            }
            string chk_sql2 = $"SELECT 1 FROM m_user_class WHERE user_cd = '{item.user_cd}' AND class_cd = '{item.class_cd}'";
            DataTable chk_dt2 = DbCommand.doSearch(chk_sql2);
            //该用户已登记到该班级
            if(chk_dt2 != null && chk_dt2.Rows.Count != 0)
            {
                return (int)SAVE_RESULT.EXISTS;
            }
            string sql = $@"
                        DELETE FROM m_user_class WHERE user_cd = '{item.user_cd}';
                        INSERT INTO m_user_class(user_cd,class_cd)VALUES('{item.user_cd}', '{item.class_cd}');
                        ";
            //执行失败，登陆失败
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            //执行成功
            return (int)SAVE_RESULT.SUCCESS;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int delete(userManager item)
        {
            string chk_sql = $"SELECT 1 FROM m_user WHERE user_cd = '{item.user_cd}' AND del_flg = '0'";
            DataTable chk_dt = DbCommand.doSearch(chk_sql);
            //该用户不存在
            if (chk_dt == null || chk_dt.Rows.Count == 0)
            {
                return (int)SAVE_RESULT.NOTESISTS;
            }
            //该用户基础信息以及对应其他信息删除
            string sql = $@"
                    UPDATE m_user SET del_flg = '1' WHERE user_cd = '{item.user_cd}';
                    DELETE FROM m_user_curriculum WHERE user_cd = '{item.user_cd}';
                    DELETE FROM m_user_class WHERE user_cd = '{item.user_cd}';
            ";
            //执行失败，删除失败
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }

        
    }
}
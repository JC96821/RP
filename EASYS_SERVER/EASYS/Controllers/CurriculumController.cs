using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurriculumController : ControllerBase
    {

        private enum SAVE_RESULT
        {
            SUCCESS = 0,  //保存成功
            FAILED = -1,  //保存失败
            EXISTS = -2   //课表编号已存在
        }

        /// <summary>
        /// 获取教师列表
        /// </summary>
        /// <returns></returns>
        public string getTeacherList()
        {
            string sql = $"SELECT user_cd, user_nm FROM m_user WHERE distinction = '1' AND del_flg = '0'";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public string search(curriculumManager item)
        {
            string sql = $@"
                         SELECT 
                             c.curriculum_cd,
                             c.curriculum_nm,
                             c.user_cd,
                             u.user_nm,
                             convert(c.term,char) term,
                             CASE WHEN c.monday = '00:00:00' THEN '' ELSE convert(c.monday,char) END AS monday,
                             CASE WHEN c.tuesday = '00:00:00' THEN '' ELSE convert(c.tuesday,char) END AS tuesday,
	                         CASE WHEN c.wednesday = '00:00:00' THEN '' ELSE convert(c.wednesday,char) END AS wednesday,
	                         CASE WHEN c.thursday = '00:00:00' THEN '' ELSE convert(c.thursday,char) END AS thursday,
                             CASE WHEN c.friday = '00:00:00' THEN '' ELSE convert(c.friday,char) END AS friday,
                             CASE WHEN c.saturday = '00:00:00' THEN '' ELSE convert(c.saturday,char) END AS saturday,
                             CASE WHEN c.sunday = '00:00:00' THEN '' ELSE convert(c.sunday,char) END AS sunday
                         FROM 
                             m_curriculum  c
                         LEFT JOIN m_user u
                             ON c.user_cd = u.user_cd
                         WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND c.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            if (!string.IsNullOrEmpty(item.user_cd)) sql += $" AND c.user_cd = '{item.user_cd}'";
            sql += "    ORDER BY c.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(curriculumManager item)
        {
            string chk_sql = $"SELECT 1 FROM m_curriculum WHERE curriculum_cd = '{item.curriculum_cd}'";
            DataTable dt = DbCommand.doSearch(chk_sql);
            string sql = "";
            //不存在该课程，追加
            if(dt == null || dt.Rows.Count == 0)
            {
                sql = $@"
                        INSERT INTO 
                            m_curriculum 
                        (
                            curriculum_cd, 
                            curriculum_nm, 
                            user_cd, 
                            term, 
                            monday, 
                            tuesday, 
                            wednesday, 
                            thursday, 
                            friday, 
                            saturday, 
                            sunday,
                            login_dt, 
                            login_user, 
                            upd_dt, 
                            upd_user
                        ) 
                        VALUES(
                            '{item.curriculum_cd}', 
                            '{item.curriculum_nm}', 
                            '{item.user_cd}', 
                            '{item.term}', 
                            '{item.monday}', 
                            '{item.tuesday}', 
                            '{item.wednesday}', 
                            '{item.thursday}', 
                            '{item.friday}', 
                            '{item.saturday}', 
                            '{item.sunday}',
                            '{DateTime.Now}', 
                            '{item.login_user}', 
                            '{DateTime.Now}', 
                            '{item.upd_user}');
                    ";
            }
            //已经登陆过该课程，更新
            else
            {
                //如果是新登录的情况，且用户编号有重复，那么返回错误
                if(item.exec_flg == "0")
                {
                    return (int)SAVE_RESULT.EXISTS;
                }
                string monday = string.IsNullOrEmpty(item.monday) ? "" : item.monday.Replace('：', ':');
                string tuesday = string.IsNullOrEmpty(item.tuesday) ? "" : item.tuesday.Replace('：', ':');
                string wednesday = string.IsNullOrEmpty(item.wednesday) ? "" : item.wednesday.Replace('：', ':');
                string thursday = string.IsNullOrEmpty(item.thursday) ? "" : item.thursday.Replace('：', ':');
                string friday = string.IsNullOrEmpty(item.friday) ? "" : item.friday.Replace('：', ':');
                string saturday = string.IsNullOrEmpty(item.saturday) ? "" : item.saturday.Replace('：', ':');
                string sunday = string.IsNullOrEmpty(item.sunday) ? "" : item.sunday.Replace('：', ':');
                sql = $@"
                        UPDATE
                            m_curriculum
                        SET
                            user_cd = '{item.user_cd}',
                            monday = '{monday}',
                            tuesday = '{tuesday}',
                            wednesday = '{wednesday}',
                            thursday = '{thursday}',
                            friday = '{friday}',
                            saturday = '{saturday}',
                            sunday = '{sunday}',
                            upd_dt = '{DateTime.Now}',
                            upd_user = '{item.user_cd}'
                        WHERE
                            curriculum_cd = '{item.curriculum_cd}'
                ";
            }
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int delete(curriculumManager item)
        {
            string sql = $"DELETE FROM m_curriculum WHERE curriculum_cd = '{item.curriculum_cd}'";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }
    }
}
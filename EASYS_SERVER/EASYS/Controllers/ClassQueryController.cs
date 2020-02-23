using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassQueryController : ControllerBase
    {
        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public string search(classQueryManager item)
        {
            string sql = $@"
                            SELECT
                                c.curriculum_cd,
                                c.curriculum_nm,
                                c.term,
                                CASE WHEN c.monday = '00:00:00' THEN '' ELSE convert(c.monday,char) END AS monday,
                                CASE WHEN c.tuesday = '00:00:00' THEN '' ELSE convert(c.tuesday,char) END AS tuesday,
	                            CASE WHEN c.wednesday = '00:00:00' THEN '' ELSE convert(c.wednesday,char) END AS wednesday,
	                            CASE WHEN c.thursday = '00:00:00' THEN '' ELSE convert(c.thursday,char) END AS thursday,
                                CASE WHEN c.friday = '00:00:00' THEN '' ELSE convert(c.friday,char) END AS friday,
                                CASE WHEN c.saturday = '00:00:00' THEN '' ELSE convert(c.saturday,char) END AS saturday,
                                CASE WHEN c.sunday = '00:00:00' THEN '' ELSE convert(c.sunday,char) END AS sunday
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_curriculum c
                                    ON uc.curriculum_cd = c.curriculum_cd
                            WHERE
                                uc.user_cd = '{item.user_cd}'
                        ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND c.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            sql += " ORDER BY c.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }
    }
}
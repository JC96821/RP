using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoreInquiryController : ControllerBase
    {
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string search(scoreInquiryManager item)
        {
            string sql = $@"
                            SELECT
                                uc.curriculum_cd,
                                c.curriculum_nm,
                                c.term,
                                uc.score
                            FROM
                                m_user_curriculum uc
                                LEFT JOIN m_curriculum c
                                    ON uc.curriculum_cd = c.curriculum_cd 
                            WHERE
                                uc.user_cd = '{item.user_cd}'
                        ";
            if (!string.IsNullOrEmpty(item.curriculum_cd)) sql += $" AND uc.curriculum_cd LIKE '{item.curriculum_cd}%'";
            if (!string.IsNullOrEmpty(item.curriculum_nm)) sql += $" AND c.curriculum_nm LIKE '%{item.curriculum_nm}%'";
            if (!string.IsNullOrEmpty(item.term)) sql += $" AND c.term = '{item.term}'";
            sql += " ORDER BY uc.curriculum_cd";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }
    }
}
using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MpiController : ControllerBase
    {
        private enum SAVE_RESULT
        {
            SUCCESS = 0,    //成功
            FAILED = -1     //失败
        }

        /// <summary>
        /// 加载用户信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string getUserInfo(mpiManager item)
        {
            string sql = $@"
                            SELECT
                                user_cd,
                                user_nm,
                                password,
                                tell_phone,
                                email
                            FROM 
                                m_user
                            WHERE
                                user_cd = '{item.user_cd}'
                        ";
            DataTable dt = DbCommand.doSearch(sql);
            return JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(mpiManager item)
        {
            string sql = $@"
                            UPDATE
                                m_user
                            SET
                                password = '{item.password}'
                                ,tell_phone = '{item.tell_phone}'
                                ,email = '{item.email}'
                            WHERE
                                user_cd = '{item.user_cd}'
                        ";
            if (!DbCommand.doTransacte(sql))
            {
                return (int)SAVE_RESULT.FAILED;
            }
            return (int)SAVE_RESULT.SUCCESS;
        }

        /// <summary>
        /// 文件数据提交
        /// </summary>
        /// <returns></returns>
        public int upload()
        {
            return 0;
        }
    }
}
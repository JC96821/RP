using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private enum SAVE_RESULT
        {
            SUCCESS = 0,  //成功
            FAILED = -1,  //失败
            EXISTS = -2   //用户编号已存在
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int save(registrationManager item)
        {
            string chk_sql = $"SELECT 1 FROM m_user WHERE user_cd = '{item.user_cd}'";
            DataTable dt = DbCommand.doSearch(chk_sql);
            if(dt != null && dt.Rows.Count > 0)
            {
                return (int)SAVE_RESULT.EXISTS;
            }
            string sql = $@"
                            INSERT INTO 
                                m_user
                            (
                                user_cd,	
                                user_nm,	
                                distinction,	
                                password,	
                                tell_phone,	
                                email,	
                                img,	
                                login_dt,	
                                login_user,	
                                upd_dt,	
                                upd_user,	
                                del_flg
                            )VALUES(
                                '{item.user_cd}',
                                '{item.user_nm}',
                                '{item.distinction}',
                                '{item.password}',
                                '{item.tell_phone}',
                                '{item.email}',
                                NULL,
                                '{DateTime.Now}',
                                '{item.user_cd}',
                                '{DateTime.Now}',
                                '{item.user_cd}',
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
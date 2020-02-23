using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EASYS.Models;
using EASYS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EASYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private enum SEARCH_RESULT
        {
            NOTEXISTS = -1, //用户不存在
            FAILED = -2     //密码错误
        }

        [HttpPost]
        public string LoginVerificaty(login item)
        {
            string sql = $"SELECT password, distinction FROM easys.m_user WHERE del_flg = '0' AND user_cd = '{item.user_cd}'";
            DataTable dt = DbCommand.doSearch(sql);
            //没有该用户
            if (dt == null || dt.Rows.Count == 0)
            {
                return Convert.ToInt32(SEARCH_RESULT.NOTEXISTS).ToString();
            }
            //密码错误
            if (dt.Rows[0]["password"].ToString() != item.password)
            {
                return Convert.ToInt32(SEARCH_RESULT.FAILED).ToString();
            }
            return dt.Rows[0]["distinction"].ToString();
        }
    }
}
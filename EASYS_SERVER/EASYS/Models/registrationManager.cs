using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EASYS.Models
{
    public class registrationManager
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string user_cd { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_nm { get; set; }

        /// <summary>
        /// 用户区分
        /// </summary>
        public string distinction { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string tell_phone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email { get; set; }

    }
}

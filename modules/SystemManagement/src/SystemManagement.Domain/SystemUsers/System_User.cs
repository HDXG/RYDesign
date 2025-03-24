using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.SystemUsers
{
    public  class System_User : Entity<Guid>
    {
        public System_User() { }

        public System_User(Guid id, string accountNumber, string passWord, string userName, bool isStatus)
            :base(id)
        {
            ChangeAccountNumber(accountNumber);
            ChangePassWord(passWord);
            ChangeUserName(userName);
            ChangeIsStatus(isStatus);
        }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNumber { get; set; }
        public void ChangeAccountNumber(string accountNumber)
        {
            AccountNumber = Check.NotNullOrWhiteSpace(accountNumber, "AccountNumber");
        }

      
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        public void ChangePassWord(string passWord)
        {
            PassWord = Check.NotNullOrWhiteSpace(passWord, "PassWord");
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public void ChangeUserName(string userName)
        {
            UserName = Check.NotNullOrWhiteSpace(userName, "UserName");
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public bool IsStatus { get; set; }
        public void ChangeIsStatus(bool isStatus)
        {
            isStatus=Check.NotNull<bool>(isStatus, "IsStatus");
            IsStatus = isStatus;
        }

    }
}

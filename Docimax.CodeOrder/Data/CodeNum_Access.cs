using Docimax.CodeOrder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docimax.CodeOrder.Entity;

namespace Docimax.CodeOrder.Data
{
    public class CodeNum_Access : ICodeNum
    {
        public string InitCodeNum(string prefix)
        {
            using (var entity = new Code_Entities())
            {
                var currentDay = DateTime.Now.Date;
                var newCode = entity.BaseDic_Code.FirstOrDefault(e => e.LastModifyTime < currentDay);
                newCode.LastModifyTime = DateTime.Now;
                var result = string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString("yyyyMMdd"), newCode.Code);
                var codeHistory = new BaseDic_Code_History
                {
                    CodeNum = result,
                    CreateDateTime = DateTime.Now,
                };
                entity.BaseDic_Code_History.Add(codeHistory);
                entity.SaveChanges();
                return result;
            }
        }
    }
}

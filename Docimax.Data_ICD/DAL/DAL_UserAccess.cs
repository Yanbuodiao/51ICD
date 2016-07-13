using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_UserAccess : IUserAccess
    {
        public bool ApplyIdentityVerify(VerifyIdentityModel Model)
        {
            using (var entity = new Entities_Write())
            {
                //using (TransactionScope transaction = new TransactionScope())
                //{
                //}
                //Database.BeginTransaction();
                return false;
            }
        }
    }
}

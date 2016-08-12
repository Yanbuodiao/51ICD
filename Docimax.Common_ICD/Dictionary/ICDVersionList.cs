using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common_ICD
{
    public static class ICDVersionList
    {
        private static List<ICDVersionModel> icdVersionList = new List<ICDVersionModel>();
        public static List<ICDModel> GetICDList(int icdVersionID, string queryStr)
        {
            var icdVersionTemp = icdVersionList.FirstOrDefault(e => e.ICD_VersionID == icdVersionID);
            if (icdVersionTemp == null)
            {
                IICDRepository access = new DAL_ICDRepository();
                icdVersionTemp = access.GetICDVersionWithICD(icdVersionID);
                icdVersionList.Add(icdVersionTemp);
            }
            if (icdVersionTemp != null)
            {
                if (icdVersionTemp.ICDList != null)
                {
                    return icdVersionTemp.ICDList.Where(e =>
                        e.ICD_Code.StartsWith(queryStr) ||
                        e.ICD_Name.StartsWith(queryStr) ||
                        e.PinyinShort.StartsWith(queryStr.ToUpper())).Take(6).ToList();
                }
            }
            return null;
        }
    }
}

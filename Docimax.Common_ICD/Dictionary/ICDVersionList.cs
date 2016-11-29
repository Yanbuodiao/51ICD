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
        public static List<ICDModel> GetICDList(int icdVersionID, string queryStr, int recordCount = 6)
        {
            if (icdVersionID <= 0)
            {
                return new List<ICDModel>();
            }
            var icdVersionTemp = icdVersionList.FirstOrDefault(e => e.ICD_VersionID == icdVersionID);
            if (icdVersionTemp == null)
            {
                IICDRepository access = new DAL_ICDRepository();
                icdVersionTemp = access.GetICDVersionWithICD(icdVersionID);
                if (icdVersionTemp != null)
                {
                    icdVersionList.Add(icdVersionTemp);
                }
            }
            if (icdVersionTemp != null && icdVersionTemp.ICDList != null)
            {
                if (!string.IsNullOrWhiteSpace(queryStr))
                {
                    return icdVersionTemp.ICDList.Where(e =>
                        e.ICD_Code.StartsWith(queryStr) ||
                        e.ICD_Code.StartsWith(queryStr.ToUpper()) ||
                        e.ICD_Name.StartsWith(queryStr) ||
                        e.PinyinShort.StartsWith(queryStr.ToUpper())).Take(recordCount).ToList();
                }
                //return icdVersionTemp.ICDList;
            }
            return new List<ICDModel>();
        }

        public static string GetCodeByName(int icdVersionID, string queryStr)
        {
            var resultICD = GetICDList(icdVersionID, queryStr).FirstOrDefault();
            if (resultICD == null)
            {
                return string.Empty;
            }
            return resultICD.ICD_Code;
        }
    }
}

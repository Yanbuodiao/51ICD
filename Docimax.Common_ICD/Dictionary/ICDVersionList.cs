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
                    var queryList = queryStr.Split(' ').Where(e => !string.IsNullOrWhiteSpace(e));

                    var tempICDList = icdVersionTemp.ICDList.Select(c => new ICDModel
                    {
                        CreateTime = c.CreateTime,
                        CreateUserID = c.CreateUserID,
                        DataLinks = c.DataLinks,
                        DeleteFlag = c.DeleteFlag,
                        DetialDescription = c.DetialDescription,
                        ICD_Code = c.ICD_Code,
                        ICD_Description = c.ICD_Description,
                        ICD_Name = c.ICD_Name,
                        ICD_VersionID = c.ICD_VersionID,
                        ICDID = c.ICDID,
                        ICDType = c.ICDType,
                        LastModifyStamp = c.LastModifyStamp,
                        LastModifyUserID = c.LastModifyUserID,
                        LastModifyTime = c.LastModifyTime,
                        OperateLogs = c.OperateLogs,
                        PinyinShort = c.PinyinShort,
                        HitCount = queryList.Count(f =>
                             c.ICD_Code.Contains(f) ||
                             c.ICD_Code.Contains(f.ToUpper()) ||
                             c.ICD_Name.Contains(f) ||
                             c.PinyinShort.Contains(f.ToUpper())),
                    });

                    return tempICDList.Where(t => t.HitCount > 0).OrderByDescending(e => e.HitCount).ToList();
                }
            }
            return new List<ICDModel>();
        }

        public static List<ICDModel> GetTypeAheadICDList(int icdVersionID, string queryStr, int recordCount = 6)
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
                    return icdVersionTemp.ICDList.Where(c => c.ICD_Code.Contains(queryStr) ||
                             c.ICD_Code.Contains(queryStr.ToUpper()) ||
                             c.ICD_Name.Contains(queryStr) ||
                             c.PinyinShort.Contains(queryStr.ToUpper())).Take(recordCount).ToList();
                }
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

using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
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
                    var queryRsult = icdVersionTemp.ICDList.Where(e => queryList.Any(t => e.ICD_Code.Contains(t) ||
                             e.ICD_Code.Contains(t.ToUpper()) ||
                             e.ICD_Name.Contains(t) ||
                             e.PinyinShort.Contains(t.ToUpper()))).ToList();

                    var tempICDList = queryRsult.Select(c => new ICDModel
                    {
                        ICD_Code = c.ICD_Code,
                        ICD_Name = c.ICD_Name,
                        ICD_VersionID = c.ICD_VersionID,
                        ICDID = c.ICDID,
                        PinyinShort = c.PinyinShort,
                        Property=c.Property,
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

        public static List<ICDViewModel> GetICDViewModelList(string queryStr, int type)
        {
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                var queryList = queryStr.Split(' ').Where(e => !string.IsNullOrWhiteSpace(e));
                var queryRsult = icdVersionList.Where(e => e.ICD_Type == type).SelectMany(t => t.ICDList, (t, i) => new { t.ICD_VersionName, i }).GroupBy(c => new { c.ICD_VersionName, c.i.ICDID }).
                    Select(p => new {
                        p.Key.ICD_VersionName,
                        p.Key.ICDID,
                        p.FirstOrDefault().i.ICD_Code,
                        p.FirstOrDefault().i.ICD_Name,
                        p.FirstOrDefault().i.PinyinShort,
                        p.FirstOrDefault().i.ICD_VersionID,
                        p.FirstOrDefault().i.Property}).
                        Where(e => queryList.Any(t => e.ICD_Code.Contains(t) ||
                         e.ICD_Code.Contains(t.ToUpper()) ||
                         e.ICD_Name.Contains(t) ||
                         e.PinyinShort.Contains(t.ToUpper())));

                var tempICDList = queryRsult.Select(c => new ICDViewModel
                {
                    ICD_Code = c.ICD_Code,
                    ICD_Name = c.ICD_Name,
                    ICD_VersionID = c.ICD_VersionID,
                    ICDID = c.ICDID,
                    ICD_VersionName = c.ICD_VersionName,
                    Property=c.Property,
                    HitCount = queryList.Count(f =>
                         c.ICD_Code.Contains(f) ||
                         c.ICD_Code.Contains(f.ToUpper()) ||
                         c.ICD_Name.Contains(f) ||
                         c.PinyinShort.Contains(f.ToUpper())),
                });
                return tempICDList.Where(t => t.HitCount > 0).OrderByDescending(e => e.HitCount).ToList();
            }
            return new List<ICDViewModel>();
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

        public static string GetCodeDispalyTextByClinicName(int icdVersionID, string clinicName)
        {
            if (icdVersionID <= 0)
            {
                return string.Empty;
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
                if (!string.IsNullOrWhiteSpace(clinicName))
                {
                    var queryResult = icdVersionTemp.ICDList.FirstOrDefault(e =>
                             e.ICD_Name == clinicName);

                    if (queryResult != null)
                    {
                        return string.Format("{0}-{1}", queryResult.ICD_Code, queryResult.ICD_Name);
                    }
                }
            }
            return string.Empty;
        }
    }
}

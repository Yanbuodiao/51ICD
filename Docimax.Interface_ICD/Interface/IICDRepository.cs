using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface IICDRepository
    {
        List<ICDModel> GetICD_Diagnosis_ModelList(string queryStr, int icd_VersionID);
        List<ICDModel> GetICD_Operate_ModelList(string queryStr, int icd_VersionID);
        ICDVersionModel GetICDVersionWithICD(int icd_VersionID);
        List<ICDVersionModel> GetICDVersionList(int icdType);
    }
}

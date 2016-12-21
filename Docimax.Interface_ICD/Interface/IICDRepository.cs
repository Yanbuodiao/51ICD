using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Interface
{
    public interface IICDRepository
    {
        List<ICDModel> GetICD_Diagnosis_ModelList(string queryStr, int icd_VersionID);
        List<ICDModel> GetICD_Operate_ModelList(string queryStr, int icd_VersionID);
        ICDVersionModel GetICDVersionWithICD(int icd_VersionID);
        List<ICDVersionModel> GetICDVersionList(int icdType);
        ICDModel GetICDModel(int icdID, ICDTypeEnum icdType);
        ICDExcuteResult<string> UpdateICDSummary(ICDModel model);
    }
}

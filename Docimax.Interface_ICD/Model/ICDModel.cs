
namespace Docimax.Interface_ICD.Model
{
    public class ICDModel
    {
        public int ICDID { get; set; }
        public string ICD_Code { get; set; }
        public string ICD_Name { get; set; }
        public string ICD_Description { get; set; }
        public string PinyinShort { get; set; }
        public int ICD_VersionID { get; set; }

        public string DisplayText { get { return string.Format("{0}-{1}", ICD_Code, ICD_Name); } }
    }
}

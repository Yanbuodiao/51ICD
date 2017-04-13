using System;

namespace Docimax.Interface_ICD.Model.WebChat
{
    public class WebChatUserSearchLog
    {
        public string LogID { get; set; }
        public string OpentId { get; set; }
        public string SearchFilter { get; set; }
        public DateTime SearchTime { get; set; }
        public int ICDVersionID { get; set; }
        public int ICDVersionType { get; set; }
        public string SegmentResult { get; set; }
    }
}

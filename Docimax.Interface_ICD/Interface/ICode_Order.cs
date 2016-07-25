using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface ICode_Order
    {
        CodeOrderModel GetNewCodeOrder(string userID, string serviceName);

        ICDExcuteResult SaveNewCodeOrder(CodeOrderModel newCodeOrder);
    }
}

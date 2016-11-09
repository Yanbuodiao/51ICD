using Docimax.Common_ICD.Message;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.UploadModel;

namespace Docimax.Common_ICD.Verify
{
    public class BaseVerify
    {
        public static ExcuteResult RequiredValidate<T>(UploadModel<T> model) where T : class
        {
            if (model == null)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.RequestNull,
                };
            }
            if (string.IsNullOrWhiteSpace(model.TicketID))
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.TicketNull,
                };
            }
            return new ExcuteResult
            {
                IsSuccess = true,
            };
        }
    }
}

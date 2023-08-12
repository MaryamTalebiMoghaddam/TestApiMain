using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TestApi.Models
{
    public class ResultModel<T>
    {
        //[JsonIgnore]
        //public MethodBase TargetSite { get; set; }
        //public ResultModel(Exception ex)
        //{
        //    Errors = new List<object> { ex };
        //    TargetSite = ex.TargetSite;
        //}
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<object>? Errors { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public void SetResultProperties(bool isSuccess, T data, List<object>? errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
            StatusCode = isSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            Message = isSuccess ? "Operation successful." : "Operation failed.";
        }
    }
}

namespace BootcampDay3InterfaceApi.Dtos
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, string message = "Success", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
}

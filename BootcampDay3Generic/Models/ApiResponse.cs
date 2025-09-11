namespace BootcampDay3Generic.Models
{
    // Generic DTO
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; } = "";

        public static ApiResponse<T> Ok(T data, string message = "Success")
            => new ApiResponse<T> { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string message)
            => new ApiResponse<T> { Success = false, Data = default, Message = message };
    }
}

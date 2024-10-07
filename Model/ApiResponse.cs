public class ApiResponse<T>
    {
        public bool Success { get; set; }         // Indicates if the operation was successful
        public string Message { get; set; }       // A message providing more details
        public T Data { get; set; }               // The actual data returned
    }
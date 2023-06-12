namespace Spotify.DTO.Response
{
    /// <summary>
    /// Generic class used for all API responses
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Response { get; set; }
    }
}

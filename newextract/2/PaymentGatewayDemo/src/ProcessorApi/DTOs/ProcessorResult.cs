namespace ProcessorApi.DTOs;

public class ProcessorResult
{
    public bool Approved { get; set; }
    public string ProcessorResponseCode { get; set; } = string.Empty;
    public string AuthCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

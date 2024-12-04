namespace FeatureToggle.Application.DTOs
{
    public class AddUserResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = [];
    }
}

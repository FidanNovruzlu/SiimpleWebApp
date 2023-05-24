namespace SiimpleWebApp.ViewModels.UsSectionVM
{
    public class UpdateUsSectionVM
    {
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public string? IconName { get; set; } 
        public IFormFile Image { get; set; } = null!;
        public string? ImageName { get; set; }
    }
}

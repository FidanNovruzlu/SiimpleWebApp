namespace SiimpleWebApp.ViewModels.UsSectionVM
{
    public class CreateUsSectionVM
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string IconName { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}

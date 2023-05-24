using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SiimpleWebApp.DAL;
using SiimpleWebApp.Models;

namespace SiimpleWebApp.ViewCompanents;

public class FooterViewComponent : ViewComponent
{
    private readonly SiimpleDbContect _siimpleDbContect;
    public FooterViewComponent(SiimpleDbContect siimpleDbContect)
    {
        _siimpleDbContect = siimpleDbContect;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> data = await _siimpleDbContect.Settings.ToDictionaryAsync(k => k.Key);
        return View(data);
    }
}

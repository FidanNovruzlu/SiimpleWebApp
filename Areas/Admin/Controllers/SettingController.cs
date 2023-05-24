using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiimpleWebApp.DAL;
using SiimpleWebApp.Models;
using SiimpleWebApp.ViewModels.SettingVM;
using System.Data;

namespace SiimpleWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{
    private readonly SiimpleDbContect _siimpleDbContect;

    public SettingController(SiimpleDbContect siimpleDbContect)
    {
        _siimpleDbContect = siimpleDbContect;
    }
    public IActionResult Index()
    {
        List<Setting> settingList = _siimpleDbContect.Settings.ToList();
        return View(settingList);
    }
    public IActionResult Update(int id)
    {
        Setting? setting= _siimpleDbContect.Settings.Find(id);
        if(setting==null) return NotFound();

        UpdateSettingVM updateSettingVM = new UpdateSettingVM()
        {
            Value = setting.Value,
        };
        return View(updateSettingVM);
    }
    [HttpPost]
    public IActionResult Update(int id,UpdateSettingVM settingVM)
    {
        Setting? setting = _siimpleDbContect.Settings.Find(id);
        if (setting == null) return NotFound();

        if(!ModelState.IsValid)
        {
            return View(settingVM);
        }

        setting.Value = settingVM.Value;

        _siimpleDbContect.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}

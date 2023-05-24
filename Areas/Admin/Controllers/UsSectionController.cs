using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiimpleWebApp.DAL;
using SiimpleWebApp.Models;
using SiimpleWebApp.ViewModels.UsSectionVM;
using System.Data;

namespace SiimpleWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsSectionController : Controller
{
    private readonly SiimpleDbContect _siimpleDbContect;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UsSectionController(SiimpleDbContect siimpleDbContect,IWebHostEnvironment webHostEnvironment)
    {
        _siimpleDbContect = siimpleDbContect;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<UsSection > usSections= _siimpleDbContect.UsSections.ToList();
        return View(usSections);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateUsSectionVM createUsSectionVM)
    {
        if (!ModelState.IsValid)
        {
            return View(createUsSectionVM);
        }

        UsSection usSection = new UsSection()
        {
            Title= createUsSectionVM.Title,
            Description= createUsSectionVM.Description,
            IconName=createUsSectionVM.IconName
        };
        if(!createUsSectionVM.Image.ContentType.Contains("image/") && createUsSectionVM.Image.Length/1024 > 2048)
        {
            ModelState.AddModelError("", "Incorrect image size or type.");
            return View(createUsSectionVM);
        }

        string newFilename= Guid.NewGuid().ToString() + createUsSectionVM.Image.FileName;
        string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets", "img",newFilename);
        using(FileStream file =new FileStream(path, FileMode.CreateNew))
        {
            createUsSectionVM.Image.CopyTo(file);
        }
        usSection.ImageName = newFilename;

        _siimpleDbContect.UsSections.Add(usSection);
        _siimpleDbContect.SaveChanges();    
        return RedirectToAction(nameof(Index));
    }
    public async Task< IActionResult> Update(int id)
    {
        UsSection? usSection= await _siimpleDbContect.UsSections.FirstOrDefaultAsync(u=>u.Id== id);
        if (usSection == null) return NotFound();

        UpdateUsSectionVM updateUsSection = new UpdateUsSectionVM()
        {
            Title=usSection.Title,
            Description=usSection.Description,
            IconName=usSection.IconName,
            ImageName=usSection.ImageName
        };

        return View(updateUsSection);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, UpdateUsSectionVM updateUsSection)
    {
        if (!ModelState.IsValid)
        {
            return View(updateUsSection);
        }

        UsSection? usSection = _siimpleDbContect.UsSections.FirstOrDefault(u => u.Id == id);
        if (usSection == null) return NotFound();


        if(updateUsSection.Image!= null)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", usSection.ImageName);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                updateUsSection.Image.CopyTo(file);
            }
        }
        if (!updateUsSection.Image.ContentType.Contains("image/") && updateUsSection.Image.Length / 1024 > 2048)
        {
            ModelState.AddModelError("", "Incorrect image size or type.");
            return View(updateUsSection);
        }

        usSection.Title = updateUsSection.Title;
        usSection.Description = updateUsSection.Description;
        usSection.IconName = updateUsSection.IconName;


        _siimpleDbContect.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        UsSection? usSection = _siimpleDbContect.UsSections.FirstOrDefault(u => u.Id == id);
        if (usSection == null) return NotFound();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", usSection.ImageName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        _siimpleDbContect.UsSections.Remove(usSection);
        _siimpleDbContect.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Read(int id)
    {

        UsSection? usSection = _siimpleDbContect.UsSections.Find(id);
        if (usSection == null) return NotFound();


        return View(usSection);
    }
}

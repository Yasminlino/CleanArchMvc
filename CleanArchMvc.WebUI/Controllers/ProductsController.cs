using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _enviroment;
    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _enviroment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _productService.GetProducts();
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryID = 
            new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        if(ModelState.IsValid)
        {
            await _productService.Add(productDTO);
            return RedirectToAction(nameof(Index));
        }

        return View(productDTO);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if(id == 0)
            return NotFound();

        var productDTO = await _productService.GetById(id);

        if(productDTO == null)
            return NotFound();
        
        var categories = await _categoryService.GetCategories();

        ViewBag.CategoryID = new SelectList(categories, "Id", "Name", productDTO.CategoryId);
        return View(productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDTO)
    {
       if(ModelState.IsValid)
        {

            await _productService.Update(productDTO);
            return RedirectToAction(nameof(Index));
        }

        return View(productDTO);
        
    }

    [HttpGet()]
    public async Task<IActionResult> Delete(int id)
    {
        if(id == 0)
            return NotFound();

        var productDTO = await _productService.GetById(id);

        if(productDTO == null)
            return NotFound();

        return View(productDTO);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> Deleteconfirmed(int id)
    {
        await _productService.Remove(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if(id == 0)
            return NotFound();

        var productDTO = await _productService.GetById(id);
        
        if(productDTO == null)
            NotFound();

        var wwwrot = _enviroment.WebRootPath;
        var image = Path.Combine(wwwrot, "images\\", productDTO.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDTO);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Shop.Application.Repository;
using Shop.Application.Repository.IRepository;
using Shop.Domain;
using Shop.Domain.Models;

namespace Shop.Server.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ProductController(
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == 0)
            {
                return View(productVM);
            }
            else //Update
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(productVM);

            StoreProductAsset(productVM.Product, file);

            if (productVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }

            _unitOfWork.Save();

            //Method to display a message!
            TempData["Success"] = "Product created successfully";

            return RedirectToAction("Index", "Product");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product? productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            DeleteProductFile(productToBeDeleted.ImageUrl);

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

        private void StoreProductAsset(Product product, IFormFile file)
        {
            string newFilePath = UploadProductFile(file);
            DeleteProductFile(product.ImageUrl);
            product.ImageUrl = newFilePath;
        }

        private string UploadProductFile(IFormFile file)
        {
            DirectoryInfo assetsBaseDirectory = GetAssetsDirectory();
            string assetsDirectoryPath = $"{assetsBaseDirectory}";

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string relativeFilePath = Path.Combine("products", fileName);

            string filePath = Path.Combine(assetsDirectoryPath, relativeFilePath);
            using FileStream fileStream = new(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return filePath;
        }

        private void DeleteProductFile(string relativeFilePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(relativeFilePath);

            DirectoryInfo assetsBaseDirectory = GetAssetsDirectory();
            string assetsDirectoryPath = $"{assetsBaseDirectory}";
            string oldFilePath = Path.Combine(assetsDirectoryPath, relativeFilePath);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
        }

        private DirectoryInfo GetAssetsDirectory()
        {
            string? assetsDirectory =
                _configuration.GetValue<string>(ApplicationSettings.AssetsBasePath);

            string message = $"Invalid value for configuration '{nameof(ApplicationSettings.AssetsBasePath)}'.";
            ArgumentException.ThrowIfNullOrEmpty(assetsDirectory, message);

            assetsDirectory = Path.GetFullPath(assetsDirectory);
            return Directory.CreateDirectory(assetsDirectory);
        }
    }
}

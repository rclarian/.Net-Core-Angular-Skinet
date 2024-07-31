using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTOs;


namespace Skinet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        //private readonly IProductRepository _productRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(
            IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo)
        {
            //this._productRepo = productRepo;
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            if (products != null)
            {
                var productReturn = products.Select(product => new ProductToReturnDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    ProductBrand = product.ProductBrand.Name,
                    ProductType = product.ProductType.Name
                }).ToList();
                return Ok(productReturn);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product != null)
            {
                var productReturn = new ProductToReturnDto 
                { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    ProductBrand = product.ProductBrand.Name,
                    ProductType = product.ProductType.Name
                };

                return Ok(productReturn);
            }
            return NotFound();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //return Ok(await _productRepo.GetProductBrandsAsync());
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //return Ok(await _productRepo.GetProductTypesAsync());
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}

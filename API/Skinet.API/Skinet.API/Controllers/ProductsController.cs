using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTOs;
using System.Collections.Generic;


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
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            //this._productRepo = productRepo;
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            if (products != null)
            {
                return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
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
                return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
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

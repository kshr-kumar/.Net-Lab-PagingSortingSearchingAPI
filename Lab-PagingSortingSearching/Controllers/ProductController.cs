using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppUI.Models;


namespace Lab_PagingSortingSearching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<List<Product>> GetProducts()
        {
            var query = _context.Products.AsQueryable();
            var data= query.ToList();
            return data;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts(String? SearchText="", int page = 1,int pageSize=4,string sortCol = "ProductId", string sortDir="DESC" )
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = _context.Products.Where(p => p.Name.Contains(SearchText)).AsQueryable();
            }
            if(page > 1)
                page = 1;

            switch(sortCol)
            {
                case "Name":
                    query = sortDir == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "Description": 
                    query = sortDir == "asc" ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description); 
                    break;
                case "Color":
                    query = sortDir == "asc" ? query.OrderBy(p => p.Color) : query.OrderByDescending(p => p.Color); 
                    break;
                case "UnitPrice": 
                    query = sortDir == "asc" ? query.OrderBy(p => p.UnitPrice) : query.OrderByDescending(p => p.UnitPrice);
                    break;
                default: 
                    query = sortDir == "asc" ? query.OrderBy(p => p.ProductId) : query.OrderByDescending(p => p.ProductId); 
                    break;
            }

            int totalRecords = query.ToList().Count();
            var data = query.Skip((page-1)*pageSize).Take(pageSize).ToList();
            return data;
        }

    }
}

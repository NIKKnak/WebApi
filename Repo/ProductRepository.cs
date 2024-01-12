using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using WebApi.Abstraction;
using WebApi.Models;
using WebApi.Models.DTO;

namespace WebApi.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddGroup(GroupDto group)
        {


            using (var context = new StoreContext())
            {
                var entityGroup = context.Groups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                    entityGroup = _mapper.Map<Models.Group>(group);
                    context.Groups.Add(entityGroup);
                    context.SaveChanges();
                    _cache.Remove("groups");
                }
                return entityGroup.Id;
            }
        }

        public int AddProduct(ProbuctDto product)
        {
            using (var context = new StoreContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Models.Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<GroupDto> GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<GroupDto> groups))
            {
                return groups;

            }

            using (var context = new StoreContext())
            {
                var groupsList = context.Groups.Select(x => _mapper.Map<GroupDto>(x)).ToList();
                _cache.Set("groups", groupsList, TimeSpan.FromMinutes(30));
                return groupsList;
            }
        }

        public IEnumerable<ProbuctDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProbuctDto> products))
            {
                return products;

            }
            using (var context = new StoreContext())
            {
                var productList = context.Products.Select(x => _mapper.Map<ProbuctDto>(x)).ToList();
                _cache.Set("product", productList, TimeSpan.FromMinutes(30));
                return productList;

            }
        }


    }
}

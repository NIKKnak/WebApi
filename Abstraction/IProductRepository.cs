using WebApi.Models;
using WebApi.Models.DTO;

namespace WebApi.Abstraction
{

    public interface IProductRepository
    {
        public int AddGroup(GroupDto group);

        public IEnumerable<GroupDto> GetGroups();

        public int AddProduct(ProbuctDto product);

        public IEnumerable<ProbuctDto> GetProducts();

    }
}

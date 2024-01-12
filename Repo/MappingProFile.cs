using AutoMapper;
using WebApi.Models;
using WebApi.Models.DTO;


namespace WebApi.Repo
{
    public class MappingProFile : Profile
    {


        public MappingProFile()     
        {
            CreateMap<Product,ProbuctDto>(MemberList.Destination).ReverseMap();
            CreateMap<Group,GroupDto>(MemberList.Destination).ReverseMap();
            CreateMap<Store,StoreDto>(MemberList.Destination).ReverseMap();    
        }


    }
}

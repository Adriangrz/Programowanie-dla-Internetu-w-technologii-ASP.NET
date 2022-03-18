using AutoMapper;
using NetworkOfShops.Models;

namespace NetworkOfShops.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<ProductCreateOrEditViewModel, Product>();
        }
    }
}

using AutoMapper;
using NetworkOfShops.Models;

namespace NetworkOfShops.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductCreateOrEditViewModel>();
            CreateMap<Shop, ShopViewModel>();
        }
    }
}

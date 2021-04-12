using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(d => d.Altura, opt => opt.MapFrom(src => src.Dimensoes.Altura))
                .ForMember(d => d.Largura, opt => opt.MapFrom(src => src.Dimensoes.Largura))
                .ForMember(d => d.Profundidade, opt => opt.MapFrom(src => src.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}

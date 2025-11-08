using AutoMapper;
using CatalogoAPI.Models;

namespace CatalogoAPI.DTOs.Mapping;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}

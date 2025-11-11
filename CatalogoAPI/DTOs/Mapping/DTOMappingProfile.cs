using AutoMapper;
using CatalogoAPI.DTOs.CategoriaDTO;
using CatalogoAPI.DTOs.ProdutoDTO;
using CatalogoAPI.Models;

namespace CatalogoAPI.DTOs.Mapping;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Produto, ProdutoDTORequest>().ReverseMap();
        CreateMap<Produto, ProdutoDTOResponse>().ReverseMap();
        CreateMap<Categoria, CategoriaDTORequest>().ReverseMap();
        CreateMap<Categoria, CategoriaDTOResponse>().ReverseMap();
    }
}

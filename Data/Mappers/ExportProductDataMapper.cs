using AutoMapper;
using ExportStatistics.Data.Models;
using ExportStatistics.Data.Entities;
public class ExportProductDataMapper : Profile
{
    public ExportProductDataMapper()
    {
        CreateMap<ExportStatistic, ExportData>()
        .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
        .ForMember(dest => dest.HsCode, opt => opt.MapFrom(src => src.Product.HsCode))
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
        .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId))
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<ExportData, ExportStatistic>();
    }
}
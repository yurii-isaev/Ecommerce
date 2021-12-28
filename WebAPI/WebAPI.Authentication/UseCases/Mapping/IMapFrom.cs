using AutoMapper;

namespace WebAPI.Authentication.UseCases.Mapping
{
  /// <summary>
  /// Provides a default generic type mapping implementation.
  /// </summary>
  public interface IMapFrom<T>
  {
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
  }
}

using System.Collections.Generic;

namespace WebAPI.Authentication.UseCases.Types
{
  public static class RoleNameTypes
  {
    public const string Сustomer = "Сustomer";
    public const string Manager = "Manager";
    public const string Administrator = "Administrator";

    public static IEnumerable<string> AllRoles
    {
      get
      {
        yield return Сustomer;
        yield return Manager;
        yield return Administrator;
      }
    }
  }
}

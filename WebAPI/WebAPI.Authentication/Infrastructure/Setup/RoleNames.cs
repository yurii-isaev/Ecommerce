using System.Collections.Generic;

namespace WebAPI.Authentication.Infrastructure.Setup;

public static class RoleNames
{
  public const string Customer = "Customer";
  public const string Manager = "Manager";
  public const string Administrator = "Administrator";

  public static IEnumerable<string> AllRoles
  {
    get
    {
      yield return Customer;
      yield return Manager;
      yield return Administrator;
    }
  }
}

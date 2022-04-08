namespace WebAPI.Authentication.UseCases.Types;

public static class Messages
{
  public const string ErrorMessage = @"An error occured seeding the database with test messages, Error:";
  public const string NullReference = "Object reference not set to an instance of an object.";

  public const string RegistrationSuccess = "User has been registered.";
  public const string RegistrationFailed = "User has been not registered.";

  public const string TokenGenerated = "Token generated.";
  public const string InvalidEmailOrPassword = "Invalid email or password.";
  
  public const string AuthSuccess = "User is authenticated";
  public const string LogoutSuccess = "You've successfully logged out";

  public const string UserNotExist = "The user doesn't exist.";
  
  public const string PasswordChangedSuccess = "Password changed successful.";
  public const string PasswordChangedFailed = "Password changed failed.";
  public const string ServerError = "Internal server error: ";

  public const string MailCollectLink = "The link to collect the password was sent to the mail";
  public const string SendMessageFailed = "Failed to send a message to this address";
  
  public const string InvalidAuthentication = "User is not authenticated or has invalid authentication data";
  public const string OrderCreatedSuccess = "Order created successfull";
  public const string OrderDeleteSuccess = "Order delete successfull";
  public const string GetOrderListSuccess = "Get order list successfull";
  public const string GetProductListSuccess = "Get product list successfull";
  
  public const string JwtCookiesKey = "jwt-cookies";
}

using System.Threading.Tasks;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.UseCases.Contracts;

public interface IOrderRepository
{
  Task CreateOrderAsync(Order order);
}

using BuberDinner.Domain.Menus;

namespace BuberDinner.Application.Common.Interfaces.Persistance;

public interface IMenuRepository
{
    void Add(Menu menu);
}
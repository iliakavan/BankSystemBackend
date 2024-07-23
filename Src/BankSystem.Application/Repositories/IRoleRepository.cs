using BankSystem.Domain.Models;
using static BankSystem.Application.Repositories.IRoleRepository;
using System;
namespace BankSystem.Application.Repositories;

public interface IRoleRepository
{
    Task<Role?> FindAsync(int roleId);
}


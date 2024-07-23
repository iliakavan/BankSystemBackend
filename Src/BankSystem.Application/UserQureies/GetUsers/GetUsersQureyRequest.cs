using BankSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.UserQureies.GetUsers;

public class GetUsersQureyRequest : IRequest<List<User>>
{
}

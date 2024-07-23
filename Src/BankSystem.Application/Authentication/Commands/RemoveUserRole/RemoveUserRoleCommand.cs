using BankSystem.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Authentication.Commands.RemoveUserRole
{
    public class RemoveUserRoleCommand : IRequest<ResultResponse>
    {
        public int Id { get; set; }
    }
}

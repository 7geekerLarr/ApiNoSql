using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlApplication.Client.Commands
{
    public class DeleteClientCommand
    {
        public class DeleteClient : IRequest
        {
            public string? ClientId { get; set; }
        }
       
    }
}

using ApiNoSqlDomain.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlApplication.Client.Commands
{
    public class UpdateClientCommand
    {
        #region  UpdateClient
        public class UpdateClient : IRequest<ClientModels>
        {
            public string? ClientId { get; set; }
            public int Level { get; set; }
            public string? Tipo { get; set; }
            public string? Name { get; set; }
            public string? Lastname { get; set; }
            public string? Dni { get; set; }
            public string? NroCliente { get; set; }
            public System.DateTime Birthdate { get; set; }
        }
        #endregion
    }
}

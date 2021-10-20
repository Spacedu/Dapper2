using Dapper.FluentMap.Mapping;
using eCommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Mappers
{
    public class UsuarioTwoMap : EntityMap<UsuarioTwo>
    {
        public UsuarioTwoMap()
        {
            Map(p => p.Cod).ToColumn("Id");
            Map(p => p.NomeCompleto).ToColumn("Nome");
            Map(p => p.NomeCompletoMae).ToColumn("NomeMae");
            Map(p => p.Situacao).ToColumn("SituacaoCadastro");
        }
    }
}

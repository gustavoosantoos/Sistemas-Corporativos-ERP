using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Utils.Identity
{
    public static class ErpRoleNames
    {
        public const string SuperAdmin = "SuperAdmin";

        public const string Administrativo = "Administrativo";

        public const string SupervisorDeCompras = "SupervisorDeCompras";
        public const string Compras = "Compras";

        public const string SupervisorDeEstoque = "SupervisorDeEstoque";
        public const string Estoquista = "Estoque";
    }

    public static class ErpRoleGroups
    {
        public const string Estoque 
            = ErpRoleNames.SuperAdmin + "," + ErpRoleNames.SupervisorDeEstoque +"," + ErpRoleNames.Estoquista;

        public const string Compras
            = ErpRoleNames.SuperAdmin + "," + ErpRoleNames.SupervisorDeCompras + "," + ErpRoleNames.Compras;

        public static readonly string Administrativo 
            = ErpRoleNames.SuperAdmin + "," + ErpRoleNames.Administrativo;
    }
}

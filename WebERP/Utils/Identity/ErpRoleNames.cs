using System;
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

        public const string SupervisorDeEstoque = "SupervidorDeEstoque";
        public const string Estoquista = "Estoquista";
    }

    public static class ErpRoles
    {
        public static ErpRole RoleForName(string roleName)
        {
            switch (roleName)
            {
                //Role para super adminstradores
                case ErpRoleNames.SuperAdmin:
                    return new ErpRole(roleName, "Super Admin", null, 0);

                //Roles do setor administrativo
                case ErpRoleNames.Administrativo:
                    return new ErpRole(roleName, roleName, Departamento.Administracao, 1);

                //Roles do setor de compras
                case ErpRoleNames.SupervisorDeCompras:
                    return new ErpRole(roleName, "Supervisor de Compras", Departamento.Compras, 1);
                case ErpRoleNames.Compras:
                    return new ErpRole(roleName, roleName, Departamento.Compras, 2);


                case ErpRoleNames.SupervisorDeEstoque:
                    return new ErpRole(roleName, "Supervisor de Estoque", Departamento.Estoque, 1);
                case ErpRoleNames.Estoquista:
                    return new ErpRole(roleName, roleName, Departamento.Estoque, 2);
                // Default
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

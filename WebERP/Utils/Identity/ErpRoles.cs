using System;
using System.Collections.Generic;
using System.Linq;

namespace WebERP.Utils.Identity
{
    public static class ErpRoles
    {
        public static ErpRole SuperAdmin = new ErpRole("SuperAdmin", "Super Admin", null, 0);

        public static ErpRole Administrativo = new ErpRole("Administrativo", "Administrativo", Departamento.Administracao, 1);

        public static ErpRole SupervisorDeCompras = new ErpRole("SupervisorDeCompras", "Supervisor de Compras", Departamento.Compras, 1);
        public static ErpRole Compras = new ErpRole("Compras", "Compras", Departamento.Compras, 2);

        public static ErpRole SupervisorDeEstoque = new ErpRole("SupervisorDeEstoque", "Supervisor de Estoque", Departamento.Estoque, 1);
        public static ErpRole Estoque = new ErpRole("Estoque", "Estoque", Departamento.Estoque, 2);

        public static List<ErpRole> GetAllRoles(Departamento? departamento = null)
        {
            var roles = new List<ErpRole>()
            {
                SuperAdmin, Administrativo, SupervisorDeCompras, Compras, SupervisorDeEstoque, Estoque
            };

            if (departamento != null)
                roles = roles.Where(e => e.Departamento == departamento).ToList();

            return roles;
        }
        
        public static ErpRole RoleForName(string roleName)
        {
            var role = GetAllRoles().FirstOrDefault(e => e.RoleName == roleName);

            if (role == null)
                throw new ArgumentException("Role inexistente.");

            return role;
        }
    }
}
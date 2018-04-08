using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebERP.Models;

namespace WebERP.Utils.Identity
{
    public class ErpRolesManager
    {
        #region Static Members
        public static ErpRole SuperAdmin = new ErpRole("SuperAdmin", "Super Admin", null, 0);

        public static ErpRole Administrativo = new ErpRole("Administrativo", "Administrativo", Departamento.Administracao, 1);

        public static ErpRole SupervisorDeCompras = new ErpRole("SupervisorDeCompras", "Supervisor de Compras", Departamento.Compras, 1);
        public static ErpRole Compras = new ErpRole("Compras", "Compras", Departamento.Compras, 2);

        public static ErpRole SupervisorDeEstoque = new ErpRole("SupervisorDeEstoque", "Supervisor de Estoque", Departamento.Estoque, 1);
        public static ErpRole Estoque = new ErpRole("Estoque", "Estoque", Departamento.Estoque, 2);
        #endregion
        
        #region Static Methods
        public static List<ErpRole> GetAllRoles(Departamento? departamento = null)
        {
            var allRoles = typeof(ErpRolesManager)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(e => e.FieldType == typeof(ErpRole)).Select(e => (ErpRole)e.GetValue(null))
                .ToList();

            if (departamento != null)
                allRoles = allRoles.Where(e => e.Departamento == departamento).ToList();

            return allRoles;
        }

        public static ErpRole RoleForName(string roleName)
        {
            var role = GetAllRoles().FirstOrDefault(e => e.RoleName == roleName);

            if (role == null)
                throw new ArgumentException("Role inexistente.");

            return role;
        } 

        public static ErpRole GetHigherRole(List<string> roles)
        {
            return roles.Select(RoleForName).OrderBy(e => e.Nivel).FirstOrDefault();
        }
        #endregion
    }
}

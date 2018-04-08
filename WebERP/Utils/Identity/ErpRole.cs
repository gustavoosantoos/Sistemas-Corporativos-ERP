using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Utils.Identity
{
    public class ErpRole
    {
        public ErpRole(string roleName, string formmatedName, Departamento? departamento, int nivel)
        {
            RoleName = roleName;
            FormmatedName = formmatedName;
            Departamento = departamento;
            Nivel = nivel;
        }

        public string RoleName { get; set; }
        public string FormmatedName { get; set; }
        public Departamento? Departamento { get; set; }
        public int Nivel { get; set; }
    }
}

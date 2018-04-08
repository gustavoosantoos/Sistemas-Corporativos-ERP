using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebERP.Extensions;
using WebERP.Utils.Identity;

namespace WebERP.Tests.AuthorizationTests
{
    [TestFixture]
    public class RoleTests
    {

        [Test]
        public void TestSuperAdminHigherRole()
        {
            var rolesToTest = ErpRolesManager.GetAllRoles();
            var higherRole = ErpRolesManager.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRolesManager.SuperAdmin, higherRole);
        }

        [Test]
        public void TestSupervisorComprasAsHigherRole()
        {
            var rolesToTest = ErpRolesManager.GetAllRoles(Departamento.Compras);
            var higherRole = ErpRolesManager.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRolesManager.SupervisorDeCompras, higherRole);
        }

        [Test]
        public void TestSupervisorEstoqueAsHigherRole()
        {
            var rolesToTest = ErpRolesManager.GetAllRoles(Departamento.Estoque);
            var higherRole = ErpRolesManager.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRolesManager.SupervisorDeEstoque, higherRole);
        }

        [Test]
        public void TestSupervisorComprasEEstoqueHigherRoles()
        {
            var rolesToTest = new List<ErpRole>()
            {
                ErpRolesManager.SupervisorDeCompras,
                ErpRolesManager.Estoque
            };

            var higherRole = ErpRolesManager.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());
            Assert.AreEqual(ErpRolesManager.SupervisorDeCompras, higherRole);
        }

        [Test]
        public void TestAvailableRoles()
        {
            var superAdminRole = ErpRolesManager.SuperAdmin;
            var availableRoles = superAdminRole.AuthorizedRolesToCreate();

            var allRoles = ErpRolesManager.GetAllRoles();
            
            allRoles.ForEach(e => Assert.Contains(e, availableRoles));
        }
    }
}

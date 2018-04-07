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
            var rolesToTest = ErpRoles.GetAllRoles();
            var higherRole = IdentityExtensions.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRoles.SuperAdmin, higherRole);
        }

        [Test]
        public void TestSupervisorComprasAsHigherRole()
        {
            var rolesToTest = ErpRoles.GetAllRoles(Departamento.Compras);
            var higherRole = IdentityExtensions.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRoles.SupervisorDeCompras, higherRole);
        }

        [Test]
        public void TestSupervisorEstoqueAsHigherRole()
        {
            var rolesToTest = ErpRoles.GetAllRoles(Departamento.Estoque);
            var higherRole = IdentityExtensions.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());

            Assert.AreEqual(ErpRoles.SupervisorDeEstoque, higherRole);
        }

        [Test]
        public void TestSupervisorComprasEEstoqueHigherRoles()
        {
            var rolesToTest = new List<ErpRole>()
            {
                ErpRoles.SupervisorDeCompras,
                ErpRoles.Estoque
            };

            var higherRole = IdentityExtensions.GetHigherRole(rolesToTest.Select(e => e.RoleName).ToList());
            Assert.AreEqual(ErpRoles.SupervisorDeCompras, higherRole);
        }

        [Test]
        public void TestAvailableRoles()
        {
            var superAdminRole = ErpRoles.SuperAdmin;
            var availableRoles = superAdminRole.AuthorizedRolesToCreate();

            var allRoles = ErpRoles.GetAllRoles();
            
            allRoles.ForEach(e => Assert.Contains(e, availableRoles));
        }
    }
}

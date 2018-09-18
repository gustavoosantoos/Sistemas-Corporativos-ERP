namespace WebERP.Utils.Identity
{
    public static class ErpRoleNames
    {
        public const string SuperAdmin = "SuperAdmin";

        public const string Administrativo = "Administrativo";

        public const string SupervisorDeCompras = "SupervisorDeCompras";
        public const string Compras = "Compras";

        public const string SupervisorDeEstoque = "SupervisorDeEstoque";
        public const string Estoque = "Estoque";

        public const string SupervisorDeVendas = "SupervisorDeVendas";
        public const string Vendedor = "Vendedor";
    }

    public static class ErpRoleGroups
    {
        public const string Estoque = 
            ErpRoleNames.SuperAdmin + "," + ErpRoleNames.SupervisorDeEstoque +"," + ErpRoleNames.Estoque;

        public const string Compras =
            ErpRoleNames.SuperAdmin + "," + ErpRoleNames.SupervisorDeCompras + "," + ErpRoleNames.Compras;

        public const string Administrativo = 
            ErpRoleNames.SuperAdmin + "," + ErpRoleNames.Administrativo;

        public const string Vendas =
            ErpRoleNames.SuperAdmin + "," + ErpRoleNames.SupervisorDeVendas + "," + ErpRoleNames.Vendedor;

        public static string[] RolesEstoque => new[] { ErpRoleNames.SuperAdmin, ErpRoleNames.SupervisorDeEstoque, ErpRoleNames.Estoque};
        public static string[] RolesCompras => new[] { ErpRoleNames.SuperAdmin, ErpRoleNames.SupervisorDeCompras, ErpRoleNames.Compras};
        public static string[] RolesAdministrativas => new[] { ErpRoleNames.SuperAdmin, ErpRoleNames.Administrativo};
        public static string[] RolesVendas => new[] { ErpRoleNames.SuperAdmin, ErpRoleNames.SupervisorDeVendas, ErpRoleNames.Vendedor };
    }
}

namespace GISA.OcelotApiGateway.Settings
{
    public static class AuthSettings
    {
        private static string _associadoSecret = "fedaf7d8863b48e197b9287d492b708e";
        private static string _prestadorSecret = "hjdak7d8863b48e257b7287d492b328f";
        private static string _acessoLegadoSecret = "hjdak7d8863b48e257b7287d492b328f";
        private static string _conveniadoSecret = "wydak7d8862b47e457b8217d462b328p";

        public static string AssociadoSecret { get => _associadoSecret; set => _associadoSecret = value; }
        public static string PrestadorSecret { get => _prestadorSecret; set => _prestadorSecret = value; }
        public static string AcessoLegadoSecret { get => _acessoLegadoSecret; set => _acessoLegadoSecret = value; }
        public static string ConveniadoSecret { get => _conveniadoSecret; set => _conveniadoSecret = value; }
    }
}

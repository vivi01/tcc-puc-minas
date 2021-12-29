namespace GISA.OcelotApiGateway
{
    public static class Settings
    {
        private static string associadoSecret = "fedaf7d8863b48e197b9287d492b708e";
        private static string conveniadoSecret = "hjdak7d8863b48e257b7287d492b328f";

        public static string AssociadoSecret { get => associadoSecret; set => associadoSecret = value; }
        public static string ConveniadoSecret { get => conveniadoSecret; set => conveniadoSecret = value; }
    }
}

namespace login.auth
{
    public class JWTAutentication
    {
        public string Key { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
    }
}

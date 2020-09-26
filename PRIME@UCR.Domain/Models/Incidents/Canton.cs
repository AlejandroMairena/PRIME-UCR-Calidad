namespace PRIME_UCR.Domain.Models
{
    public class Canton
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Provincia Provincia { get; set; }
    }
}
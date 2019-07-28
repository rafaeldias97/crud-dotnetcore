using System;

namespace CRUDMongoDb.Models
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public DateTime dataNascimento { get; set; }
        public DateTime dataCriacao { get; set; } = DateTime.Now;
    }
}

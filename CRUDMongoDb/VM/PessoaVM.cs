using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMongoDb.VM
{
    public class PessoaVM
    {
        public Guid Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataNascimento { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMongoDb.VM
{
    public class PessoaCadastrarVM
    {
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataNascimento { get; set; }
    }
}

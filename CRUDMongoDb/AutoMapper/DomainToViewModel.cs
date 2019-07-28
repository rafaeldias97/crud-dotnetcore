using AutoMapper;
using CRUDMongoDb.AutoMapper.VM;
using CRUDMongoDb.Models;
using CRUDMongoDb.VM;

namespace CRUDMongoDb.AutoMapper
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<PessoaLoginVM, Pessoa>();
            CreateMap<PessoaVM, Pessoa>();
            CreateMap<PessoaCadastrarVM, Pessoa>();
        }
    }
}

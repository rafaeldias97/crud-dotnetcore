using AutoMapper;
using CRUDMongoDb.AutoMapper.VM;
using CRUDMongoDb.Models;
using CRUDMongoDb.VM;

namespace CRUDMongoDb.AutoMapper
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            CreateMap<Pessoa, PessoaLoginVM>();
            CreateMap<Pessoa, PessoaVM>();
            CreateMap<Pessoa, PessoaCadastrarVM>();
        }
    }
}

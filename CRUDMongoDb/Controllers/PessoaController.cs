using AutoMapper;
using CRUDMongoDb.AutoMapper.VM;
using CRUDMongoDb.Context;
using CRUDMongoDb.JWT;
using CRUDMongoDb.Models;
using CRUDMongoDb.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace CRUDMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly MongoContext dbContext = new MongoContext();
        private IMapper mapper;

        public PessoaController(IMapper _mapper)
        {
            mapper = _mapper;
        }
        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            var entity = dbContext.pessoas.Find(x => true).ToList();
            var _entity = mapper.Map<IEnumerable<PessoaVM>>(entity);
            return Ok(_entity);
        }

        [HttpPost("Auth")]
        public IActionResult Autenticar([FromBody] PessoaLoginVM pessoa, [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            var res = dbContext.pessoas.Find(x => x.email == pessoa.email 
                            && x.senha == pessoa.senha).FirstOrDefault();
            if (res.Id != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(res.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, res.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, res.email),
                        new Claim(JwtRegisteredClaimNames.Birthdate, res.dataNascimento.ToString())
                    }
                ); ;

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);
                return Ok(token);
            } else
            {
                return BadRequest(new { ErrorMessage = "Usuario Inválido" });
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] PessoaCadastrarVM pessoa)
        {
            var _pessoa = mapper.Map<Pessoa>(pessoa);
            dbContext.pessoas.InsertOne(_pessoa);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] PessoaVM pessoa)
        {
            var res = dbContext.pessoas.Find(x => x.Id == pessoa.Id).FirstOrDefault();
            var _pessoa = mapper.Map<Pessoa>(pessoa);
            _pessoa.senha = res.senha;
            _pessoa.dataCriacao = res.dataCriacao;
            dbContext.pessoas.ReplaceOne(p => p.Id == _pessoa.Id, _pessoa);
            return Ok(pessoa);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            dbContext.pessoas.DeleteOne(x => x.Id == id);
        }
    }
}

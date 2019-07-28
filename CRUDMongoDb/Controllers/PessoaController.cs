using AutoMapper;
using CRUDMongoDb.AutoMapper.VM;
using CRUDMongoDb.Context;
using CRUDMongoDb.JWT;
using CRUDMongoDb.Models;
using CRUDMongoDb.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly MongoContext dbContext = new MongoContext();
        private IMapper mapper;
        public IConfiguration Configuration { get; }

        public PessoaController(IMapper _mapper, IConfiguration _Configuration)
        {
            mapper = _mapper;
            Configuration = _Configuration;
        }
        // GET api/values/5
        [HttpGet]
        [Authorize()]
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
            var conexao = dbContext.pessoas.Find(x => x.email == pessoa.email
                            && x.senha == pessoa.senha).FirstOrDefault();
            if (conexao != null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, conexao.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, conexao.email),
                    new Claim(JwtRegisteredClaimNames.Birthdate, conexao.dataNascimento.ToString())
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: tokenConfigurations.Issuer,
                    audience: tokenConfigurations.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            return BadRequest(new
            {
                ErrorMessage = "Credenciais Inválidas...",
                Code = 401
            });

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
        [Authorize()]
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
        [Authorize()]
        public void Delete(Guid id)
        {
            dbContext.pessoas.DeleteOne(x => x.Id == id);
        }
    }
}

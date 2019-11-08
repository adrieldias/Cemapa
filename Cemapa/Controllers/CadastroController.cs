﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Cemapa.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace Cemapa.Controllers
{
    public class CadastroController : ApiController
    {
        private Entities db = new Entities();
        
        //[Route("API/Cadastro/GetPersonalizado")]
        [HttpGet]
        public System.Web.Mvc.JsonResult GetPersonalizado()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from c in db.TB_CADASTRO
                            //.Include("TB_CIDADE")  
                            .AsNoTracking()
                        orderby c.DT_CADASTRO descending
                        //select c //traz todos os campos
                        select new
                        {
                            CODIGO = c.COD_CADASTRO,
                            TIPO = c.TB_TIPO_CADASTRO.DESC_TIPO_CADASTRO,
                            NOME = c.NOME,                            
                            TELEFONE = c.DESC_TELEFONE,
                            CELULAR = c.DESC_CELULAR,
                            CNPJ_CPF = c.NUM_CGC_CPF,
                            ENDERECO = c.DESC_ENDERECO,
                            //CodCidade = c.COD_CIDADE,
                            CIDADE = c.TB_CIDADE.DESC_CIDADE,
                            BAIRRO = c.DESC_BAIRRO,
                            INSCRICAO = c.NUM_INSCRICAO,
                            FANTASIA = c.DESC_FANTASIA,
                            CLASSIFICACAO = c.TB_CLASS_CADASTRO.DESC_CLASSIFICACAO                            
                        }).Take(100);

            return new System.Web.Mvc.JsonResult()
            {
                Data = query,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public IList<TB_CADASTRO> Get([FromBody] TB_CADASTRO cadastro)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from c in db.TB_CADASTRO
                        where c.NOME.StartsWith(cadastro.NOME)
                        orderby c.DT_CADASTRO descending
                        select c;
            return query.ToList();                        
        }

        //[Route("API/Cadastro/GetPersonalizado")]
        [HttpPost]
        public System.Web.Mvc.JsonResult GetPersonalizado([FromBody] TB_CADASTRO cadastro)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from c in db.TB_CADASTRO
                             //.Include("TB_CIDADE") 
                             .AsNoTracking()
                         where c.NOME.StartsWith(cadastro.NOME)
                         orderby c.DT_CADASTRO descending
                         //select c //traz todos os campos
                         select new
                         {
                             CODIGO = c.COD_CADASTRO,
                             TIPO = c.TB_TIPO_CADASTRO.DESC_TIPO_CADASTRO,
                             NOME = c.NOME,
                             TELEFONE = c.DESC_TELEFONE,
                             CELULAR = c.DESC_CELULAR,
                             CNPJ_CPF = c.NUM_CGC_CPF,
                             ENDERECO = c.DESC_ENDERECO,
                             //CodCidade = c.COD_CIDADE,
                             CIDADE = c.TB_CIDADE.DESC_CIDADE,
                             BAIRRO = c.DESC_BAIRRO,
                             INSCRICAO = c.NUM_INSCRICAO,
                             FANTASIA = c.DESC_FANTASIA,
                             CLASSIFICACAO = c.TB_CLASS_CADASTRO.DESC_CLASSIFICACAO
                         });

            return new System.Web.Mvc.JsonResult()
            {
                Data = query,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

        //[Route("api/Cadastro/Get/{id}")]
        public TB_CADASTRO Get(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;           
            var query = (from c in db.TB_CADASTRO                                 
                                //.Include("TB_USUARIO")
                            where (id == null || c.COD_CADASTRO == id)
                            orderby c.DT_CADASTRO
                            select c).FirstOrDefault();
            return query;            
        }

        [HttpPost]
        public System.Web.Mvc.JsonResult Save([FromBody] TB_CADASTRO cadastro)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var id = cadastro.COD_CADASTRO;
                    if (db.TB_CADASTRO.Any(c => c.COD_CADASTRO == id))
                    {                        
                        cadastro.DT_ALTERACAO = System.DateTime.Now;
                        db.Entry(cadastro).State = EntityState.Modified;                        
                    }
                    else
                    {   
                        cadastro.COD_CADASTRO = db.Database.SqlQuery<int>("SELECT SQCADASTRO.nextval FROM dual").First();
                        cadastro.IND_SEXO_CATEGORIA = "M";
                        cadastro.DT_CADASTRO = System.DateTime.Now;
                        db.Entry(cadastro).State = EntityState.Added;
                    }
                    // Os models atrelados ao cadastro, por exemplo, endereço, propriedade, ainda não estão sendo salvos.
                    db.SaveChanges();
                    return new System.Web.Mvc.JsonResult()
                    {
                        Data = cadastro.COD_CADASTRO,
                        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                    };
                }
                catch(DbEntityValidationException e)
                {
                    string erro = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {   
                        foreach (var ve in eve.ValidationErrors)
                        {   
                            erro += string.Format("{0} - {1}", ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    return new System.Web.Mvc.JsonResult()
                    {
                        Data = erro,
                        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                    };
                } 
                catch(Exception e)
                {
                    return new System.Web.Mvc.JsonResult()
                    {
                        Data = string.Format("{0} - {1}", e.Message, e.InnerException),
                        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = "Modelo Inválido",
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public System.Web.Mvc.JsonResult Delete(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            try
            {
                TB_CADASTRO query = (from c in db.TB_CADASTRO
                            //.Include("TB_CADASTRO_ENDERECOS")
                            //.Include("TB_PROPRIEDADE")                            
                            where c.COD_CADASTRO == id
                            select c).FirstOrDefault();
                db.Entry(query).State = EntityState.Deleted;
                db.SaveChanges();

                var res = "Deletado";
                return new System.Web.Mvc.JsonResult()
                {
                    Data = res,
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                };
            }
            catch(Exception ex)
            {
                var err = string.Format("{0} - {1}", ex.Message, ex.InnerException);
                return new System.Web.Mvc.JsonResult()
                {
                    Data = err,
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                };
            }
        }
    }

   
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.Data.Repositories
{
    public class SolicitacaoRepository : BaseRepository<Solicitacao>
    {
        public SolicitacaoRepository(ApplicationDbContext context) : base(context)
        {
        }


        /// <summary>
        /// Pega a última solicitação do product que esteja em aberto.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Solicitacao PegaUltimaSolicitacaoDoProduto(int productId)
        {
            var solicitacao = All().Include(e => e.Solicitante)
                .OrderByDescending(e => e.Data)
                .FirstOrDefault(e => e.ProdutoId == productId);
            return solicitacao;
        }

        public bool ProdutoPossuiSolicitacaoAberta(int productId)
        {
            var possuiSolicitacao = All().Any(e => e.ProdutoId == productId && !e.IsSolicitacaoFinalizada());
            return possuiSolicitacao;
        }

        public List<Solicitacao> ListAll()
        {
            return All()
                .Include(e => e.Produto)
                .Include(e => e.Solicitante)
                .Include(e => e.Orcamentos)
                .ToList();
        }

        public List<Solicitacao> ListaSolicitacoesEmOrcamentacao()
        {
            return All()
                .Include(e => e.Produto)
                .Include(e => e.Solicitante)
                .Where(e => e.Status == StatusSolicitacao.Orcamentacao)
                .ToList();
        }

        public Solicitacao FindById(int id, bool includeProduto = false, bool includeSolicitante = false, bool includeOrcamentos = false)
        {
            var solicitacoes = All();

            if (includeProduto)
                solicitacoes = solicitacoes.Include(e => e.Produto);
            if (includeSolicitante)
                solicitacoes = solicitacoes.Include(e => e.Solicitante);
            if (includeOrcamentos)
                solicitacoes = solicitacoes.Include(e => e.Orcamentos);
            
            return solicitacoes.FirstOrDefault(e => e.Id == id);
        }

        public void AdicionarOrcamentoParaSolicitacao(int solicitacaoId, Orcamento orcamento)
        {
            Solicitacao solicitacao = FindById(solicitacaoId);

            if (solicitacao == null)
                throw new ArgumentException("A solicitação não foi encontrada.");
            
            _context.Solicitacoes.Update(solicitacao);
            _context.SaveChanges();
        }
    }
}

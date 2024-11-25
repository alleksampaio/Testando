﻿using AppInvest.Domain.Interfaces.Repositories;
using AppInvest.Domain.Interfaces.Services;
using AppInvest.Domain.Models;
using AppInvest.Domain.Models.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvest.Domain.Services
{
    public class AcoesService : IAcoesService
    {
        private readonly IAcoesRepository _acoesRepository;

        public AcoesService(IAcoesRepository acoesRepository)
        {
            _acoesRepository = acoesRepository;
        }

        public async Task<IEnumerable<Acoes>> BuscarAcoes()
        {
            return await _acoesRepository.BuscarAcoes();
        }

        public async Task<Acoes> BuscarAcoesId(int Id)
        {
            return await _acoesRepository.Get(x => x.Id == Id);
        }

        public async Task<Acoes> CadastrarAcoes(Acoes acoes)
        {
            await _acoesRepository.CadastrarAcoes(acoes);
            await _acoesRepository.UnitOfWork.SaveChangesAsync();

            return acoes;
        }

        public async Task<Acoes> AtualizarAcoes(AtualizarAcoesCommand command)
        {
            var acoes = await _acoesRepository.Get(x => x.Id == command.Id);
            if (acoes == null) return null;

            acoes.Atualizar(command.Nome,
                command.Quantidade,
                command.Pm,
                command.PmIr,
                command.Dividendos,
                command.TotalInv);

            await _acoesRepository.AtualizarAcoes(acoes);
            await _acoesRepository.UnitOfWork.SaveChangesAsync();
            return acoes;

        }

        public async Task<bool> DeletarAcoes(int Id)
        {
            var acoes = await _acoesRepository.Get(x => x.Id == Id);
            if (acoes == null) return false;

            await _acoesRepository.DeletarAcoes(acoes);
            await _acoesRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }


    }
}

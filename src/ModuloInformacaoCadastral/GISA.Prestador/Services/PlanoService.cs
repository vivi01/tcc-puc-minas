﻿using GISA.EventBusRabbitMQ.Interfaces;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GISA.Prestador.Services
{
    public class PlanoService : IPlanoService
    {
        public IPlanoRepository _planoRepository;
        private IBus _busControl;

        public PlanoService(IPlanoRepository planoRepository, IBus busControl)
        {
            _planoRepository = planoRepository;
            _busControl = busControl;
        }

        public void Adicionar(Plano plano)
        {
            _planoRepository.Add(plano);
        }

        public void Editar(Plano plano)
        {
            _planoRepository.Update(plano);
        }

        public Plano ObterPorId(int id)
        {
            return _planoRepository.GetById(id);
        }

        public void Deletar(Plano plano)
        {
            _planoRepository.Delete(plano);
        }

        public List<Plano> ObterTodos()
        {
            return _planoRepository.Get().ToList();
        }
    }
}
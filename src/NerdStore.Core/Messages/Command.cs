using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages
{
    //O mediator vai mediar a troca de mensagens, através de encontrar o
    //manipulador daquela mensagem, através do recurso de injeção de dependência do ASP.NET Core
    //o IRequest diz que esta vai ser uma mensagem do tipo Request, ou seja, 
    //vou solicitar alguma coisa
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        //ValidationResult do FluentValidation é uma coleção de validações em formato
        //de resultado

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();

        }
    }
}

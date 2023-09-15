using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Queries;

public static class ListBirthdayChildren
{

    public class Query : IRequest<IList<Person>>
    {
        public DateTime Date { get; set; }
    }

    public class Validator : AbstractValidator<Query> {
        public Validator()
        {
            RuleFor(q => q.Date)
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, IList<Person>>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<IList<Person>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _arpaContext.Persons
                    .Where(p => p.DateOfBirth.HasValue 
                        && p.DateOfBirth.Value.Month == request.Date.Month 
                        && p.DateOfBirth.Value.Day == request.Date.Day 
                        && p.MusicianProfiles.Any())
                    .ToListAsync(cancellationToken);
        }
    }

}

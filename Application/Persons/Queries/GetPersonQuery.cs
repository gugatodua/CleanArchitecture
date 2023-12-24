using Application.CustomExceptions;
using Application.Persons.Queries.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }
    }

    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer _localizer;
        private readonly IMapper _mapper;

        public GetPersonQueryHandler(IPersonRepository personRepository, IStringLocalizer localizer, IMapper mapper)
        {
            _personRepository = personRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);

            if (person == null)
            {
                var errorMessageKey = "PersonNotFound";
                var message = _localizer[errorMessageKey].ToString();

                throw new PersonNotFoundException(HttpStatusCode.NotFound, message);
            }

            return _mapper.Map<PersonDto>(person);
        }
    }
}
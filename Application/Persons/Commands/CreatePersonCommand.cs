using Application.Persons.Queries.DTOs;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;

namespace Application.Persons.Commands
{
    public class CreatePersonCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberDto> PhoneNumberDtos { get; set; }
        public List<RelatedPersonDto> RelatedPersonDtos { get; set; }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var phoneNumbers = _mapper.Map<List<PhoneNumber>>(request.PhoneNumberDtos);
            var relatedPeople = _mapper.Map<List<RelatedPerson>>(request.RelatedPersonDtos);

            var person = new Domain.Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                PhoneNumbers = phoneNumbers,
                RelatedPeople = relatedPeople,
                CityId = request.CityId
            };

            await _personRepository.CreateAsync(person);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
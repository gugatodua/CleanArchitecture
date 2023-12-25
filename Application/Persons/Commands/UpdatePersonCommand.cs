using Application.CustomExceptions;
using Application.Persons.Queries.DTOs;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Persons.Commands
{
    public class UpdatePersonCommand : IRequest<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalId { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;
        private readonly IMapper _mapper;

        public UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IStringLocalizer localizer, IMapper mapper)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var person = await _personRepository.GetPersonDbModelByIdAsync(request.Id);

            try
            {
                if (person == null)
                {
                    throw new PersonNotFoundException(System.Net.HttpStatusCode.NotFound, _localizer["PersonNotFound"]);
                }

                person.BirthDate = request.BirthDate;
                person.CityId = request.CityId;
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Gender = request.Gender;
                person.PersonalId = request.PersonalId;
                person.PhoneNumbers = _mapper.Map<List<PhoneNumber>>(request.PhoneNumbers);

                _personRepository.Update(person);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return person;
        }
    }
}
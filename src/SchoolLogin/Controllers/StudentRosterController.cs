using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SchoolLogin.Data;
using SchoolLogin.Dtos;
using SchoolLogin.Models;

namespace SchoolLogin.Controllers
{
    // [Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class StudentRosterController : ControllerBase 
    {
        private readonly ISchoolloginRepo _repository;
        private readonly IMapper _mapper;
  
        public StudentRosterController(ISchoolloginRepo repository, IMapper mapper) //constructor for dependency to be injected
        {
            _repository = repository;       //injected
            _mapper = mapper;
        }   //dependency injection

        [HttpGet]
        public async Task<IEnumerable<SchoolLoginReadDto>> GetAllCommands()
        {
            var studentlist = (await _repository.GetAllCommands()).Select(StudentRoster => _mapper.Map<SchoolLoginReadDto>(StudentRoster));
            return studentlist;
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<SchoolLoginReadDto>> GetCommandById(int id)
        {
            var student = await _repository.GetCommandById(id);   
            if(student != null)
            {
                return _mapper.Map<SchoolLoginReadDto>(student);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SchoolLoginCreateDto>> CreateCommand(SchoolLoginCreateDto schoolLoginCreateDto)
        {
            var studentroster = _mapper.Map<StudentRoster>(schoolLoginCreateDto);
            await _repository.CreateCommand(studentroster);
            _repository.SaveChanges();
            return CreatedAtAction(nameof(GetCommandById), new {id = studentroster.ID}, schoolLoginCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, SchoolLoginUpdateDto schoolLoginUpdateDto)
        {
            var studentModelFromRepo = await _repository.GetCommandById(id);
            if(studentModelFromRepo is null)
            {
                return NotFound();
            }
            _mapper.Map(schoolLoginUpdateDto,studentModelFromRepo);
            await _repository.UpdateCommand(studentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCommanUpdate(int id, JsonPatchDocument<SchoolLoginUpdateDto> patchDoc)
        {
            var studentModelFromRepo = await _repository.GetCommandById(id);
            if(studentModelFromRepo == null)
            {
                return NotFound();
            }
            var rostertopatch = _mapper.Map<SchoolLoginUpdateDto>(studentModelFromRepo);
            patchDoc.ApplyTo(rostertopatch,ModelState);
            if (TryValidateModel(rostertopatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(rostertopatch,studentModelFromRepo);
            await _repository.UpdateCommand(studentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var studentModelFromRepo = await _repository.GetCommandById(id);
            if(studentModelFromRepo == null)
            {
                return NotFound();
            }
            await _repository.DeleteCommand(studentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
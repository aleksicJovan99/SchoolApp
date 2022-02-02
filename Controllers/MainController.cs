using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Dto;
using SchoolApp.Interfaces;

namespace SchoolApp.Controllers;

[ApiController]
[Route("/api")]
public class MainController : ControllerBase
{
    private IGenericRepository _repository;

    public MainController(IGenericRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("school")]
    public IActionResult GetAllSchools()
    {
        var schoolDto = _repository.GetAll<School>().Select(s => new
        {
            ID = s.SchoolId,
            Name = s.SchoolName
        });
        return Ok(schoolDto);
    }
    
    [HttpPost]
    [Route("school")]
    public IActionResult InsertSchool(SchoolDto schoolDto)
    {
        var school = new School
        {
            SchoolName = schoolDto.SchoolName,
        };
        
        _repository.Insert<School>(school);
        _repository.Save();
        return Ok();
    }

    [HttpPost]
    [Route("school/range")]
    public IActionResult InsertRangeSchool(List<SchoolDto> schoolDtos)
    {
        foreach (var s in schoolDtos)
        {
            var school = new School()
            {
                SchoolName = s.SchoolName
            };
            _repository.Insert<School>(school);
        }
        _repository.Save();
        return Ok();
    }
    
    [HttpGet]
    [Route("student")]
    public IActionResult GetAllStudents()
    {
        var studentDto = _repository.GetAll<Student>()
            .Join(_repository.GetAll<School>(),
                student => student.SchoolId,
                school => school.SchoolId,
                (st, sc) => new
                {
                    ID = st.StudentId,
                    Name = st.StudentName,
                    School = sc.SchoolName
                }
            );
        
        return Ok(studentDto);
    }
    
    [HttpPost]
    [Route("student")]
    public IActionResult InsertStudent(StudentDto studentDto)
    {
        var student = new Student
        {
            StudentName = studentDto.StudentName,
            SchoolId = studentDto.SchoolId
        };
        
        _repository.Insert<Student>(student);
        _repository.Save();
        return Ok();
    }

    [HttpGet]
    [Route("class")]
    public IActionResult GetAllClasses()
    {
        var classDto = _repository.GetAll<Class>().Select(c => new
        {
            ID = c.ClassId,
            Name = c.ClassName
        });

        return Ok(classDto);
    }
    
    [HttpPost]
    [Route("class")]
    public IActionResult InsertClass(ClassDto classDto)
    {
        var classVar = new Class
        {
            ClassName = classDto.ClassName
        };
        
        _repository.Insert(classVar);
        _repository.Save();
        return Ok();
    }

    [HttpPost]
    [Route("school/class")]
    public IActionResult InsertClassInSchool(SchoolClassDto schoolClassDto)
    {
        var schoolClass = new SchoolClass()
        {
            SchoolId = schoolClassDto.SchoolId,
            ClassId = schoolClassDto.ClassId
        };
        
        _repository.Insert(schoolClass);
        _repository.Save();
        return Ok();
    }

    [HttpDelete]
    [Route("school/class/{id}")]
    public IActionResult DeleteClassInSchool(int id)
    {
        _repository.Delete<SchoolClass>(id);
        _repository.Save();
        return Ok();
    }

    [HttpGet]
    [Route("school/class")]
    public IActionResult GetAllClassInSchool()
    {
        var schoolClasses = _repository.GetAll<SchoolClass>()
            .Join(_repository.GetAll<School>(),
                sc => sc.SchoolId,
                s => s.SchoolId,
                (sc, s) => new
                {
                    s.SchoolName,
                    sc.ClassId
                }
            )
            .Join(_repository.GetAll<Class>(),
                o => o.ClassId,
                c => c.ClassId,
                (o, c) => new
                {
                    o.SchoolName,
                    c.ClassName
                }
            );
        return Ok(schoolClasses);
    }

    [HttpGet]
    [Route("school/classes")]
    public IActionResult ClassesInSchool()
    {
        var obj = _repository.GetAll<SchoolClass>()
            .Join(_repository.GetAll<School>(),
                sc => sc.SchoolId,
                s => s.SchoolId,
                (sc, s) => new
                {
                    s.SchoolName,
                    sc.SchoolId,
                    sc.ClassId
                }
            )
            .Join(_repository.GetAll<Class>(),
                o => o.ClassId,
                c => c.ClassId,
                (o, c) => new
                {
                    o.SchoolId,
                    o.SchoolName,
                    c.ClassName
                }
            )
            .GroupBy(g => g.SchoolId)
            .Select(s => new
            {
                SchoolID = s.Key,
                SchoolName = s.Select(e => e.SchoolName).First(),
                ClassName = s.Select(e => e.ClassName)
            });
        return Ok(obj);
    }

    [HttpGet]
    [Route("student/class")]
    public IActionResult GetAllStdInCls()
    {
        var sicDto = _repository.GetAll<StudentsInClass>()
            .Join(_repository.GetAll<Student>(),
                studInCls => studInCls.StudentId,
                student => student.StudentId,
                (sic, s) => new
                {
                    ID = sic.Id,
                    Student = s.StudentName,
                    ClassId = sic.ClassId
                   
                })
            .Join(_repository.GetAll<Class>(),
                    sic => sic.ClassId,
                    c => c.ClassId,
                    (sic, c) => new
                    {
                        ID = sic.ID,
                        Student = sic.Student,
                        Class = c.ClassName
                    }
            )
                
            ;
        return Ok(sicDto);
    }
    
    [HttpPost]
    [Route("student/class")]
    public IActionResult InsertStdInCls(StudentsInClassDto sicDto)
    {
        var sic = new StudentsInClass
        {
            StudentId = sicDto.StudentId,
            ClassId = sicDto.ClassId
        };
        _repository.Insert(sic);
        _repository.Save();
        return Ok();
    }

    [HttpGet]
    [Route("class/students")]
    public IActionResult StudentsInClass()
    {
        var group = _repository.GetAll<StudentsInClass>()
            .Join(_repository.GetAll<Student>(),
                sic => sic.StudentId,
                s => s.StudentId,
                (sic, s) => new
                {
                    sic.ClassId,
                    s.StudentName
                }
            ).Join(_repository.GetAll<Class>(),
                sic => sic.ClassId,
                c => c.ClassId,
                (sic, c) => new
                {
                    sic.ClassId,
                    c.ClassName,
                    sic.StudentName
                }
            )
            .GroupBy(g => g.ClassId)
            .Select(c => new
            {
                CLassId = c.Key,
                ClassName = c.Select(x => x.ClassName).First(),
                Students = c.Select(x => x.StudentName)
            })
            .OrderBy(c => c.CLassId);
        return Ok(group);
    }
}
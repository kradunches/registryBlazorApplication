global using Xunit;
using Registry.Domain.Model;
using TestProject;

public class TestPersonRepository
{
  [Fact]
  //Тест, проверяющий, что база данных создалась
  public void VoidTest()
  {
    var testHelper = new TestHelper();

    var disciplineRepository = testHelper.DisciplineRepository;

    Assert.Equal(1, 1);
  }

  [Fact]
  public void TestAdd()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;

    var discipline2 = new Discipline
    {
      Name = "Математика",
      Time = 200
    };

    disciplineRepository.AddAsync(discipline2).Wait();
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();

    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
    Assert.Equal("Математика", disciplineRepository.GetByNameAsync("Математика").Result.Name);
    Assert.Equal("Программирование", disciplineRepository.GetByNameAsync("Программирование").Result.Name);
  }

  [Fact]
  public void TestUpdate()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Программирование").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "Проектирование";
        discipline.AddLesson(new Lesson
        {
            Topic = "Name",
            LessonType = "Лекция",
        });
        disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("Проектирование", disciplineRepository.GetByNameAsync("Проектирование").Result.Name);
    }


  [Fact]
  public void TestDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Программирование").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();

    disciplineRepository.DeleteAsync(discipline.Id).Wait();




    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 0);


  }

  [Fact]
  public void TestUpdateAdd()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetDisciplineAsync("Программирование").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "Математика";
        discipline.AddLesson(new Lesson
        {
            Topic = "Новая",
            LessonType = "Лабораторная"
        });

        disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("Математика", disciplineRepository.GetDisciplineAsync("Математика").Result.Name);
    Assert.Equal(2, disciplineRepository.GetDisciplineAsync("Математика").Result.LessonCount);
  }

  [Fact]
  public void TestUpdateDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Программирование").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    var lessons = new Lesson
    {
      Topic = "2 курс",
      LessonType = "Практическое занятие",
    };
    discipline.AddLesson(lessons);
    discipline.RemoveLesson(0);

    disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal(0, disciplineRepository.GetByNameAsync("Программирование").Result.LessonCount);
  }

}